using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanker : Trailer
{
    TrailerType status = TrailerType.TANKER;
    

    void Start()
    {
        base.maxLoads(600, 50000);
        TrailerAge();
        TrailerCost();
        TrailerSpace();
    }


}
