using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferVan : Trailer
{
    TrailerType status = TrailerType.REFERVAN;
    void Start()
    {
        base.maxLoads(708, 55000);
    }
}
