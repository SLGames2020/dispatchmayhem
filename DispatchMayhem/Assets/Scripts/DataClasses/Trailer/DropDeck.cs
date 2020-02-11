using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDeck : Trailer
{
    TrailerType status = TrailerType.DROPDECK;

    void Start()
    {
        base.maxLoads(800, 96000);
        TrailerAge();
        TrailerCost();
        TrailerSpace();
    }
}
