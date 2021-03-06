﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CityTrafficLevels { LIGHT, MODERATE, HEAVY, JAMMED }

public class GameTime : MonoBehaviour
{
    private static GameTime instance = null;
    public static GameTime inst { get { return instance; } }

    public Text timeText;

    public static float[] trafficDelays = { 1.0f, 1.5f, 3.0f, 6.0f };
    public float GetTrafficDelay(int trafficlevel) { return trafficDelays[trafficlevel]; }

    [Range(0.3333f, 50.0f)]
    public float timeScale = 1.0f;

    private DateTime gTime;
     public DateTime gmTime { get { return gTime; } }
    private int      gHour;
     public int      gmHour { get { return gHour; } }
    private bool     gPM;
     public bool     gmPM   { get { return gPM; } }

    private static float gmTimeScale = 1.0f;             //base time scale is 1 minute Real Time = 1 hour Game Time (A good playerPref candidate)
    

    private void Awake()
    {
        if ((instance != null) && (instance != this))
        {
             Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gTime = System.DateTime.Now;                      //initialize with real time for now, but this should have a check for a save and restore to that
        gHour = gTime.Hour;
        gPM = true;                                       //possibly glitch prone but setting it to something for now (it's corrected in the first update frame)

        if (PlayerPrefs.HasKey("TimeScale"))
        {
            gmTimeScale = PlayerPrefs.GetFloat("TimeScale");
        }
        else
        {
            PlayerPrefs.SetFloat("TimeScale", gmTimeScale);
            PlayerPrefs.Save();
        }
        InvokeRepeating("TimeTick", 0.1f, 0.1f);            //increment the time every tenth of a second for some resolution

        timeScale = gmTimeScale;

    }

    // Update is called once per frame
    void Update()
    {
        if (gmTimeScale != timeScale)
        {
            UpdateTimeScale(timeScale, true);
            timeScale = gmTimeScale;
        }

        timeText.text = gTime.ToShortDateString() + " " + gTime.ToShortTimeString();

        //if (timeText.text.Contains("PM"))
        //{
        //    gPM = true;
        //}
        //else
        //{
        //    gPM = false;
        //}
    }

    public void OnApplicationQuit()
    {
        instance = null;
    }

    /************************************************************
        TimeTick

        This method is designed to repeat every second and 
        increment out Game "Minutes of the day". 

        By adding gmTimeScale, our orignal time ratio of 1hr/1m
        is automatically scaled.

        changed to 1/10th of a second for rapid update of the 
        date/time text field

        changed to 1 minute = 3 game hrs (jvt 30/3/20)
    *************************************************************/
    void TimeTick()
    {
        gTime = gmTime.AddMinutes(timeScale*3.0f/10.0f); //this converts timeScale minutes of game time into one second of Real Time
                                                    //(assumes 0.1 second update rate from the InvokeRepeating above)
        gHour = gTime.Hour;                         //a quick access value for course time checks
        if (gHour > 11)
        {
            gPM = true;
        }
        else
        {
            gPM = false;
        }
    }

    /***********************************************************
        UpdateTimeScale

        method for game and user changing of the time scale
        also takes care of saving the scale when flagged that
        it is a user saveable time change
        (in case their are game states that can change this)

    ************************************************************/
    void UpdateTimeScale(float newscale, bool svit)
    {
        if ((newscale >= 0.1f) && (newscale <= 50.0f))
        {
            gmTimeScale = newscale;
            if (svit)
            {
                PlayerPrefs.SetFloat("TimeScale", gmTimeScale);
                PlayerPrefs.Save();
            }
        }
    }

    /*************************************************************
        FindMySpeed

        This method takes in a speed of Unity units per hour
        (basically a road/city's Z-scale divided by its travelTime)

        As we have 1hr game time = 1 minute real time unt/h is also
        equal to unt/m in real time. Multiply this by our time scale
        and a road/city that takes one hour to cross in game time
        take 1 minute in real playing time. 

    ***************************************************************/
    public float FindMySpeed (float untph)
    {
        // unpth = unity units per hour
        return (untph *3.0f / 60.0f * gmTimeScale);
    }

    /**************************************************************
        gmHoursToRealSeconds

        This value takes in number of game hours and returns 
        a number of seconds in Real Time. 

        Primarly for coroutine waits

    ***************************************************************/
    public float gmHoursToRealSeconds(float hrs)
    {
        return (hrs * 3.0f / 60.0f * gmTimeScale);
    }
}
