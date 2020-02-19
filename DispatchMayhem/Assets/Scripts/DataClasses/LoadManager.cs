using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour
{
    

    public string originLabel;
    public Vector2 origin;
    public string destinationLabel;
    public Vector2 destination;

    public GameObject trailer;

    public float pay;
    public float space;

    public enum Products
    {
        BOXES,
        COLDGOODS,
        CONTRUCTION_LARGE,
        CONTRUCTION,
        LIQUIDS,
    }

    public Products allowed;

    public void AllowedLoads()
    {
        trailer.GetComponent<Trailer>();
        if(allowed == Products.BOXES)
        {
            
        }
        else if(allowed == Products.COLDGOODS)
        {

        }
        else if(allowed == Products.CONTRUCTION)
        {

        }
        else if(allowed == Products.CONTRUCTION_LARGE)
        {

        }
        else if(allowed == Products.LIQUIDS)
        {

        }
    }

    public void LoadChecker()
    {
        bool isAllowed = true;
        /*if(allowed == Products.LIQUIDS && Trailer.TrailerType != Trailer.TrailerType.TANKER)
        {
            isAllowed = false;
        }*/
    }

    

}
