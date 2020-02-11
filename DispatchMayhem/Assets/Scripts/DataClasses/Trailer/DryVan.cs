using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DryVan : Trailer
{
    TrailerType status = TrailerType.DRYVAN;

    void Start()
    {
        base.maxLoads(708, 55000);
        TrailerAge();
        TrailerCost();
        TrailerSpace();
    }
}