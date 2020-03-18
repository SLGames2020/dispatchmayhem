using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadM : MonoBehaviour
{
    private static LoadM instance = null;
    public static LoadM inst { get { return instance; } }

    public GameObject trailer;
    public GameObject loadPrefab;

    public string originLabel;
    public Vector2 origin;
    public string destinationLabel;
    public Vector2 destination;

    public float pay;
    public float space;

    //private enum Products
    //{
    private const int BOXES = 0;
    private const int COLDGOODS = 1;
    private const int CONTRUCTION_LARGE = 2;
    private const int CONTRUCTION = 3;
    private const int LIQUIDS = 4;
    private const int UNDEFINED = 5;             //error catch, only ever set at instantiation (always equal to TOTALPRODUCTS)
    private const int TOTALPRODUCTS = 5;
    //}

    public int allowed;
                                            //make sure to add new product labels before the "Undefined" entry
    private string[] productLabels = { "Boxes", "Cold Goods", "Construction (Large)", "Construction", "Liquids", "Undefined" };
        public string getProdcutLabel(int pidx) { return productLabels[pidx]; }
        public int productMax = 0;

    public JobsPanel panelToAddLoadsTo;

    void Awake()
    {
        //Debug.Log("City Manager Awake");

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        productMax = productLabels.Length - 1;

    }

    private void Start()
    {
        panelToAddLoadsTo = UIM.inst.JobsPanel.GetComponent<JobsPanel>();
    }


    private void OnApplicationQuit()
    {
        instance = null;
    }

    public void AllowedLoads()
    {
        space = trailer.GetComponent<Trailer>().GetMaxSpace();
        bool isAllowed = true;
        Trailer.TrailerType currType;
        if (allowed == BOXES)
        {
            currType =  trailer.GetComponent<DryVan>().GetTrailerType();
            if (currType != Trailer.TrailerType.DRYVAN)
            {
                isAllowed = false;
                Debug.Log("This product is not allowed please select the correct trailer");
            }

            pay = 100 * space;
        }
        else if(allowed == COLDGOODS)
        {
            currType = trailer.GetComponent<ReferVan>().GetTrailerType();
            if (currType != Trailer.TrailerType.REFERVAN)
            {
                isAllowed = false;
                Debug.Log("This product is not allowed please select the correct trailer");
            }

            pay = 100 * space;
        }
        else if(allowed == CONTRUCTION)
        {
            currType = trailer.GetComponent<Flatbed>().GetTrailerType();
            if (currType != Trailer.TrailerType.FLATBED)
            {
                isAllowed = false;
                Debug.Log("This product is not allowed please select the correct trailer");
            }

            pay = 100 * space;
        }
        else if(allowed == CONTRUCTION_LARGE)
        {
            currType = trailer.GetComponent<DropDeck>().GetTrailerType();
            if (currType != Trailer.TrailerType.DROPDECK)
            {
                isAllowed = false;
                Debug.Log("This product is not allowed please select the correct trailer");
            }

            pay = 100 * space;
        }
        else if(allowed == LIQUIDS)
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

    /*****************************************************************************
        CreateNewLoad

        This method is called by the cities to create new loads. It takes the 
        original Citie's GPS location and name, and will populate the remaining
        details of the load. A destination for the load is also created randomly

    ******************************************************************************/
    public void CreateNewLoad(string orlbl, Vector2 orgps)
    {
        Vector2 des;
        GameObject go;

        do
        {
            go = CyM.inst.openCities[Random.Range(0, CyM.inst.openCities.Count - 1)];
            des = go.GetComponent<MapSupport>().gps;
        }
        while (des == orgps);                     //loop till we find a city that isn't the calling city

        GameObject ldgo = Instantiate(loadPrefab, this.transform.position, Quaternion.identity);
        Load ld = ldgo.GetComponent<Load>();
        ld.originLabel = orlbl;
        ld.origin = orgps;
        ld.destinationLabel = go.GetComponent<City>().label;
        ld.destination = des;
        ld.productType = Random.Range(0, TOTALPRODUCTS);
        ld.productLabel = productLabels[ld.productType];
        ldgo.name = ld.productLabel + " (" + ld.originLabel + " to " + ld.destinationLabel +")";
        ldgo.transform.parent = this.transform;

        //if (panelToAddLoadsTo.AddToLoadList(ldgo))
        //{
        //    //loads.Add(ldgo);
        //}
        //else
        //{
        //    Destroy(ldgo);
        //    //Debug.Log("Failed to add load to list for " + label);
        //}
    }





}
