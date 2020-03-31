using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryVan : Trailer
{
    public int type = DRYVAN;
    public GameObject dryvan;
    void Start()
    {
        TrailerAge(Random.Range(1000, 5000));
        TrailerCost(20000);
        TrailerSpace(Random.Range(20000, 50000));
    }
}