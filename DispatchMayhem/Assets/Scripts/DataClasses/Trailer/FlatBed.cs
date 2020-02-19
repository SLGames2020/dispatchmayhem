using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flatbed : Trailer
{
    TrailerType status = TrailerType.FLATBED;
    public GameObject flatbed;
    void Start()
    {
        TrailerAge(Random.Range(1000, 5000));
        TrailerCost(23000);
        TrailerSpace(Random.Range(20000, 50000));
    }
}

