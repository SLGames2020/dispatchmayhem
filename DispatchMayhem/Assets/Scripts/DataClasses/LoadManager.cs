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
        space = trailer.GetComponent<Trailer>().GetMaxSpace();
        bool isAllowed = true;
        Trailer.TrailerType currType;
        if (allowed == Products.BOXES)
        {
            currType =  trailer.GetComponent<DryVan>().GetTrailerType();
            if (currType != Trailer.TrailerType.DRYVAN)
            {
                isAllowed = false;
                Debug.Log("This product is not allowed please select the correct trailer");
            }

            pay = 100 * space;
        }
        else if(allowed == Products.COLDGOODS)
        {
            currType = trailer.GetComponent<ReferVan>().GetTrailerType();
            if (currType != Trailer.TrailerType.REFERVAN)
            {
                isAllowed = false;
                Debug.Log("This product is not allowed please select the correct trailer");
            }

            pay = 100 * space;
        }
        else if(allowed == Products.CONTRUCTION)
        {
            currType = trailer.GetComponent<Flatbed>().GetTrailerType();
            if (currType != Trailer.TrailerType.FLATBED)
            {
                isAllowed = false;
                Debug.Log("This product is not allowed please select the correct trailer");
            }

            pay = 100 * space;
        }
        else if(allowed == Products.CONTRUCTION_LARGE)
        {
            currType = trailer.GetComponent<DropDeck>().GetTrailerType();
            if (currType != Trailer.TrailerType.DROPDECK)
            {
                isAllowed = false;
                Debug.Log("This product is not allowed please select the correct trailer");
            }

            pay = 100 * space;
        }
        else if(allowed == Products.LIQUIDS)
        {
            currType = trailer.GetComponent<Tanker>().GetTrailerType();
            if(currType != Trailer.TrailerType.TANKER)
            {
                isAllowed = false;
                Debug.Log("This product is not allowed please select the correct trailer");
            }

            pay = 100 * space;
        }

    }   

}
