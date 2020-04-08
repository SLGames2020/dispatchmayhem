using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferVan : Trailer
{
    //TrailerType type = TrailerType.REFERVAN;
    int type = REFERVAN;
    public GameObject refervan;
    void Start()
    {
        TrailerAge(Random.Range(1000, 5000));
        TrailerCost(22000);
        TrailerSpace(Random.Range(20000, 50000));
    }
}
