using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flatbed : Trailer
{
    TrailerType status = TrailerType.FLATBED;
    
    void Start()
    {
        base.maxLoads(400, 48000);
        TrailerAge();
        TrailerCost();
        TrailerSpace();
    }
}

