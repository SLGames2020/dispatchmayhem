using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Trailer : MonoBehaviour
{

    public enum TrailerType
    {
        FLATBED,
        DROPDECK,
        DRYVAN,
        REFERVAN,
        TANKER
    }

    public GameObject trailer;
    public TrailerType type;

    public void maxLoads(int space, int weight)
    {
         
    }
}

