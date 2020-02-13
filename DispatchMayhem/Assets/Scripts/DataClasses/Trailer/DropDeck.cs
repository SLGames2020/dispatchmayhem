using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDeck : Trailer
{
    TrailerType status = TrailerType.DROPDECK;
    public GameObject dropdeck;
    void Start()
    {
        TrailerAge(Random.Range(1000, 5000));
        TrailerCost(25000);
        TrailerSpace(Random.Range(20000, 50000));
    }
}
