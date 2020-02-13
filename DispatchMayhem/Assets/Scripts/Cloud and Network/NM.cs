using System;
using System.IO;
using System.Net.Sockets;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.Networking;

using Mapbox;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using Mapbox.Directions;
using Mapbox.Unity.Utilities;

public delegate void routeCallBack(List<Vector2> route, float distance);

public class NM : MonoBehaviour
{
    private static NM instance = null;
    public static NM Inst { get { return instance; } }


    //public routeCallBack FindRoute;
    //private Vector2[] testRoute;

    public string accessToken = "pk.eyJ1Ijoic2xnYW1lcyIsImEiOiJjazVlMm00MXYwMGxoM2ZwYnN1NjIxcjJxIn0.IGD0z3Stw1R5fXMAWpz2JA";

    private TcpClient mapboxClient;
    private StreamReader reader;
    private StreamWriter writer;

    private const string directionsURI = "https://api.mapbox.com/directions/v5/mapbox/driving/";
    private const string geometriesParam = "?geometries=geojson";
    private const string accessTokenSuffix = "&access_token=";

    private Directions routeResults;

    private Vector2 s = new Vector2(-75.6973f, 45.4215f);
    private Vector2 e = new Vector2(-74.7303f, 45.0213f);

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

        //FindRoute = MyTestCallBack;
        Debug.Log("Network Manager Awake");
    }
    // Start is called before the first frame update
    void Start()
    {
        //GetRoute(s, e, FindRoute);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnApplicationQuit()
    {
        instance = null;
    }

    /*******************************
        test callback routine

    **********************************/
    public void MyTestCallBack(List<Vector2> rte, float dst)
    {
        foreach (Vector2 pnt in rte)
        {
            Debug.Log("Waypoint List: [" + pnt.x + "," + pnt.y + "]");
        }
        Debug.Log("Distance: " + dst);
    }
    /*************************************************************
        GetRoute

        This just sets up and calls a coroutine to the actual 
        http route request, because the webrequest can take time.

        callback will be called once the route data is received
        and processed

    ***************************************************************/
    public void GetRoute(Vector2 start, Vector2 end, routeCallBack callback)
    {

        string uri = directionsURI + start.x + "," + start.y + ";" + end.x + "," + end.y 
                    + geometriesParam + accessTokenSuffix + accessToken;

        Debug.Log(uri);
        StartCoroutine(DownloadRoute(callback, uri));
    }

    /**************************************************************
        DownloadAndSetImage

        This coroutine does the actual http call route data.
        once found it strips out the "coordinates" array and 
        creates a list of Vector2s. The list, as well as the route
        distance is passed to the Call back function

        Note that this can be optomized and if I hear back from
        Mapbox about what utilities they have to extract the 
        coordinate, I will change it to use their supplied code

    ***************************************************************/
    IEnumerator DownloadRoute(routeCallBack cb, string uri)
    {
        UnityWebRequest request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();

        string dlstring = request.downloadHandler.text;

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error + " uri: " + uri);
        }
        else if (dlstring.IndexOf("NoRoute") > 0)
        {
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);                  //leaving in Debugs for now in case verifications are required

            string searchstring = ",\"distance\":";                     //find and extract the distance data (while it's convient)
            int startidx = dlstring.IndexOf(searchstring);
            int offset = searchstring.Length;
            startidx += offset;
            searchstring = ",\"duration\":";
            int endidx = dlstring.IndexOf(searchstring);
            float dist = float.Parse(dlstring.Substring(startidx, endidx - startidx));

            searchstring = "{\"coordinates\":[";                        //here we find and extract JUST the coordinates array
            startidx = dlstring.IndexOf(searchstring);
            offset = searchstring.Length;
            startidx += offset;
            searchstring = "],\"type\"";
            endidx = dlstring.IndexOf(searchstring);
            string coordarray = dlstring.Substring(startidx, endidx - startidx);

            if (coordarray.Length == 0)
            {
                //Debug.Log("Does not Contain a Waypoint list");
                dist = -1.0f;                                         //return no route data found
            }
            else
            {
                //Debug.Log("Start: " + startidx + " End: " + endidx + " Length: " + coordarray.Length);
                //Debug.Log("Waypoint List: " + coordarray);

                List<Vector2> waypoints = new List<Vector2>();
                waypoints.Clear();

                endidx = 0;
                bool scanning = true;
                while (scanning)
                {
                    startidx = 0;
                    endidx = coordarray.IndexOf(",[");                                                          //each waypoint is formated as [#.###,#.###],[#.###,#.###],... ...],
                    if (endidx < 0)                                                                             //if the next waypoint is not found
                    {
                        endidx = coordarray.Length;                                                             //just extract what is left
                        scanning = false;
                    }
                    string wypnt = coordarray.Substring(startidx, endidx - startidx);                           //wypnt is now a single [#.###,#.###] data set
                    //Debug.Log(wypnt);
                    //Debug.Log("Start: " + startidx + " End: " + endidx + " Length: " + coordarray.Length);
                    if (scanning)                                                                               //only do the substring if we have more waypoints
                    {                                                                                           //otherwise we get an error
                        coordarray = coordarray.Substring(endidx + 1, coordarray.Length - endidx - 1);
                    }

                    startidx = 1;                                                                               //now extract the individual lng and lats
                    endidx = wypnt.IndexOf(",");
                    float lng = float.Parse(wypnt.Substring(startidx, endidx - startidx));
                    startidx = endidx + 1;
                    endidx = wypnt.Length - 1;
                    float lat = float.Parse(wypnt.Substring(startidx, endidx - startidx));
                    waypoints.Add(new Vector2(lng, lat));
                }
                cb(waypoints, dist);                                                                           //once complete, send it all to the call back
            }
        }
    }



}
