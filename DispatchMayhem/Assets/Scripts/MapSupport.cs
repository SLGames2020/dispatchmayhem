using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mapbox.Examples;

public class MapSupport : MonoBehaviour
{
    public Vector2 gps;
    public Vector3 baseScale = new Vector3(200.0f, 200.0f, 200.0f);
    //private Mapbox.Examples.SpawnToMap spawntomap;
    

    void Awake()
    {
        //GameObject mapOB = MapM.inst._map;
        //spawntomap = mapOB.GetComponent<SpawnToMap>();
        gps.y = 45.016667f;                             //default all objects to cornwall for now
        gps.x = -74.733333f;                            //(this will need to be changed to a "Depot" lookup eventually)
    }

    // Start is called before the first frame update
    void Start()
    {
        //spawntomap.AddToMap(this.gameObject);
        MapM.inst.AddToMap(this.gameObject);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}
}
