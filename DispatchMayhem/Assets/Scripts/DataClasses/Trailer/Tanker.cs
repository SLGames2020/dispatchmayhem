using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanker : Trailer
{
    TrailerType type = TrailerType.TANKER;
    public GameObject tanker;

    void Start()
    {
        TrailerAge(Random.Range(1000, 5000));
        TrailerCost(28000);
        TrailerSpace(Random.Range(20000, 50000));
    }
}
