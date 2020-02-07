using System;
using System.IO;
using System.Net.Sockets;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

using Mapbox;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Directions;
using Mapbox.Unity.Utilities;


public class NM : MonoBehaviour
{
    private static NM instance = null;
    public static NM inst { get { return instance; } }

    public delegate void routeCallBack(Vector2[] route);
    public routeCallBack FindRoute;

    public string accessToken = "pk.eyJ1Ijoic2xnYW1lcyIsImEiOiJjazVlMm00MXYwMGxoM2ZwYnN1NjIxcjJxIn0.IGD0z3Stw1R5fXMAWpz2JA";


    private TcpClient mapboxClient;
    private StreamReader reader;
    private StreamWriter writer;

    private const string directionsURI = "https://api.mapbox.com/directions/v5/mapbox/driving/";
    private const string geometriesParam = "?geometries=geojson";
    private const string accessTokenSuffix = "&access_token=";

    private Directions routeResults;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnApplicationQuit()
    {
        instance = null;
    }

    /*************************************************************
        GetRoute

        This just sets up and calls a coroutine to the actual 
        http image request, because the webrequest can take time.

        (and because we have the actual game object the coroutine
        can update the character whenever they're ready

    ***************************************************************/
    public void GetRoute(Vector2 start, Vector2 end, routeCallBack callback)
    {

        string uri = directionsURI + start.x + "," + start.y + ";" + end.x + "," + end.y + geometriesParam + accessToken;
        StartCoroutine(DownloadRoute(callback, uri));
    }

    /**************************************************************
        DownloadAndSetImage

        This coroutine does the actual http call for the webimage.
        once found it will update the passed PC's image.

        Note that this is an overlay image and does not change the
        character's sprite or animation.

    ***************************************************************/
    IEnumerator DownloadRoute(routeCallBack cb, string uri)
    {
        UnityWebRequest request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError) Debug.Log(request.error + " uri: " + uri);
        else
        {
            routeResults = JsonUtility.FromJson<Directions>(request.downloadHandler.text);


            Texture2D tx2d = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite newsprt = Sprite.Create(tx2d, new Rect(0.0f, 0.0f, tx2d.width, tx2d.height), new Vector2(0.5f, 0.5f), 100.0f);
            cb(newsprt);
        }
    }



}
