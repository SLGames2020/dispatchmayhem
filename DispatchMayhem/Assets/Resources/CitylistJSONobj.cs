
using System;
using UnityEngine;

//this came from here (https://simplemaps.com/data/ca-cities) and is free as long as we credit them
//other countries list can also be found there when required
[Serializable]
public class CityJSONobj
{
    public string city;
    public string admin;
    public string country;
    public int population_proper;
    public string iso2;
    public string capital;
    public float lat;
    public float lng;
    public int population;

    public Vector2 GetGPS() { return (new Vector2(lng, lat)); }
}

[Serializable]
public class CitylistJSONobj
{
    public CityJSONobj[] cities;
}
