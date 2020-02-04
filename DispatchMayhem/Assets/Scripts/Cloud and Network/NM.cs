using System;
using System.IO;
using System.Net.Sockets;
using System.Collections;
using System.ComponentModel;
using System.Collections.Generic;

using UnityEngine;

using MapBox;
using Mapbox.Utils;
using Mapbox.Unity.Map;
using MapBox.Directions;
using Mapbox.Unity.Utilities;


public class NM : MonoBehaviour
{
    private static NM instance = null;
    public static NM inst { get { return instance; } }

    public string accessToken = "pk.eyJ1Ijoic2xnYW1lcyIsImEiOiJjazVlMm00MXYwMGxoM2ZwYnN1NjIxcjJxIn0.IGD0z3Stw1R5fXMAWpz2JA";


    private TcpClient mapboxClient;
    private StreamReader reader;
    private StreamWriter writer;

    private const string directionsURI = "https://api.mapbox.com/directions/v5/mapbox/driving/";
    private const string geometriesParam = "geometries=geojson";
    private const string accessTokenSuffix = "&access_token=";

    private DirectionResult routeResults;


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
}
