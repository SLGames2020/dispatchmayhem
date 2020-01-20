using System;
using System.IO;
using System.Net.Sockets;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;

public class TwitchChat : MonoBehaviour
{

    private TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    public string username;
    public string password;         //get password from https://twitchapps.com/tmi  (oauth?)
    public string channelName;

    [Header ("Game Properties")]
    public GameObject playerPrefab;
    public GameObject crowdContainer;
    public GameObject heartLauncher;

    private GetURLImages getUrlImages;

    private string chatHeader;

    private float lastSecond;

   
    //private int skipper = 0;
    
    //private Vector2 speed;
    
    void Start()
    {
        chatHeader = ":" + username + "!" + username + "@" + username + ".tmi.twitch.tv PRIVMSG #" + channelName + " :";
        getUrlImages = this.gameObject.GetComponent<GetURLImages>();
        Connect();
        //lastSecond = (int) (Time.time+0.5f);
        //twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
    }

    void Update()
    {
        if (twitchClient == null)               //This get's nulled sometimes (IDE bug?)
        {                                       //but do the check seperately as any other
            Connect();                          //logic check might trigger a "null reference" error
        }
        else if (!twitchClient.Connected) 
        {
            if (Time.time > lastSecond)
            {
                lastSecond = Time.time + 0.5f;
                Connect();
            }
        }
        else
        {
            ReadChat();
        }
        /*
        //skipper++;// = 0;
        if ((lastSecond < Time.time) && ((int)Time.time % 600)==0)
        {
            lastSecond = (int)(Time.time + 2.0f);
        }
        else if ((lastSecond > Time.time)) // && ((skipper % 50)==0) )
        //if ((skipper % 100)== 0)
        {
            //lastSecond = Time.time + 1.5f;
            float ran = UnityEngine.Random.Range(1, 100);
            float ranIt = ran; // +5;
            /*if (ran > 5)
            {
                ran = UnityEngine.Random.Range(1, 10);
                ranIt *= ran;
                if (ran > 5)
                {
                    ran = UnityEngine.Random.Range(1, 10);
                    ranIt *= ran;
                    if (ran > 5)
                    {
                        ran = UnityEngine.Random.Range(1, 10);
                        ranIt *= ran;
                    }
                }
            }

            string spwnstrng = GM.inst.spawnTrigger + ranIt;
            string namestring = ranIt.ToString(); // "me" + ranIt;

            GameInputs(namestring, spwnstrng);
        }
    */
    }

    private void Connect()
    {

        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        writer.WriteLine("PASS " + password);
        writer.WriteLine("NICK " + username);
        writer.WriteLine("USER " + username + " 8 * : " + username);
        writer.WriteLine("JOIN #" + channelName);
        writer.Flush();

        if (twitchClient.Connected)
        {
            Debug.Log("Twitch Connection Ok");
            writer.WriteLine(chatHeader + "Sy_Bits Connected. Let the Rope Wars begin!");
            writer.Flush();
        }
        else Debug.Log("Twitch Connection Failed");
        //sendIrcMessage(":" + userName + "!" + userName + "@" + userName + ".tmi.twitch.tv PRIVMSG #" + channel + " :" + message);
        //        writer.WriteLine("I'm Ready to play!");
        //        writer.Flush();
    }

    private void ReadChat()
    {
        if (twitchClient.Available > 0)
        {
            var message = reader.ReadLine();
            if (message.Contains("PRIVMSG"))
            {
                var splitPoint = message.IndexOf("!", 1);
                var chatName = message.Substring(0, splitPoint);
                chatName = chatName.Substring(1);

                splitPoint = message.IndexOf(":", 1);
                message = message.Substring(splitPoint + 1);
                Debug.Log(chatName + " " + message);
                //print(String.Format("{0}: {1}", chatName, message));

                GameInputs(chatName, message);
            }
            else if (message.Contains("PING"))
            {
                writer.WriteLine("PONG :tmi.twitch.tv");
                writer.Flush();
                Debug.Log("Pong!");
            }
        }
    }

    private void GameInputs(string name, string cmd)
    {
        Debug.Log(name + " " + cmd);
        int cheers = Regex.Matches(cmd, GM.inst.spawnTrigger).Count;
        Debug.Log(name + " " + cmd + " " + GM.inst.spawnTrigger + " " + cheers);
        if (cheers > 0)
        {
            int bits = GM.inst.GetIntegerFromString(cmd);      //only returns the first int

            CharControl cc = null;
            GameObject pc = GM.inst.FindPCwithName(name);

            if (pc != null)
            {
                if (pc.transform.position.y < GM.inst.uSpawn.y)         //only fire the hearts if the player is on the screen
                {
                    GameObject hrtl = Instantiate(heartLauncher, GM.inst.uSpawn, Quaternion.identity);
                    HeartLauncher hl = hrtl.GetComponent<HeartLauncher>();
                    hl.cheerSize = bits;
                    hl.heartCount = cheers;
                    hl.targetPC = pc;
                }
                else
                {
                    pc.GetComponent<CharControl>().AddToFullStrength(bits*cheers);
                    GM.inst.ForceNewTeamSpeed();
                }
            }
            else
            {
                pc = Instantiate(playerPrefab, GM.inst.uSpawn, Quaternion.identity); //final position and direction set in prefab start
                pc.transform.SetParent(crowdContainer.transform, true);
                cc = pc.GetComponent<CharControl>();

                string url = getUrlImages.FindPlayerURL(name);
                if (url != "")
                {
                    getUrlImages.GetURLTexture(cc.SetSprite, url);
                }

                cc.SetFullStrength(bits*cheers);
                cc.SetName(name);

                SpriteRenderer spriteRen = pc.GetComponent<SpriteRenderer>();
                //TextMeshPro textMesh = pc.Find("TextMeshPro"); //TODO: get the text sorting order to match the sprite

                GM.inst.AddPC(pc);
                if (GM.inst.Intermission()) cc.animator.SetBool("allowedToPlay", true);
            }
        }
    }

    /************************************************************
        SpawnPC

        Just a simple method to create new PC in the game.

        Team, states and list assignment is done elsewhere

    *************************************************************/
/*  private GameObject SpawnPC(string name, int bits)
    {
        GameObject pc = Instantiate(playerPrefab, GM.inst.uSpawn, Quaternion.identity); //final position and direction set in prefab start
        pc.transform.SetParent(crowdContainer.transform, false);
        CharControl cc = pc.GetComponent<CharControl>();
        cc.SetStrength(bits);
        cc.SetName(name);

        return (pc);
    }
*/
}
