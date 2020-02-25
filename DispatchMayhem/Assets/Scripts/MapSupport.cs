using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mapbox.Examples;

public class MapSupport : MonoBehaviour
{
    public Vector2 gps;
    public Mapbox.Examples.SpawnToMap spawntomap;
    

    void Awake()
    {
        GameObject mapOB = GameObject.Find("Map");
        spawntomap = mapOB.GetComponent<SpawnToMap>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spawntomap.AddToMap(this.gameObject);
    }

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}
}
