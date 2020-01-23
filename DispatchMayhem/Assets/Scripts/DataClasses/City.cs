using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City
{
    public List<Load> loads;
    public string label = "unknown city";

    public float lat;
    public float lon;

    public class Ontario:City
    {
        void Ottawa()
        {
            lat = 0;
            lon = 0;
        }

        void Cornwall()
        {
            lat = 5;
            lon = 5;
        }

        void Kingston()
        {
            lat = -5;
            lon = -5;
        }
    }
    


    // Start is called before the first frame update
    //void Start()
    //{
    //    
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}
}
