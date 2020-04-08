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
    private const int BOXES = 0;                //using const ints because Enums are difficult to use randomly
    private const int COLDGOODS = 1;
    private const int CONSTRUCTION_LARGE = 2;
    private const int CONSTRUCTION = 3;
    private const int LIQUIDS = 4;
    private const int UNDEFINED = 5;             //error catch, only ever set at instantiation (always equal to TOTALPRODUCTS)
    private const int TOTALPRODUCTS = 5;
    //}

    public int allowed;
    //make sure to add new product labels before the "Undefined" entry
    private static string[] productLabels = { "Boxes", "Cold Goods", "Construction (Large)", "Construction", "Liquids", "Undefined" };
    public static string getProductLabel(int pidx) { return productLabels[pidx]; }
    public int productMax = 0;

    public JobsPanel panelToAddLoadsTo;

    void Awake()
    {
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
    /*
    public void AllowedLoads()
    {
        space = trailer.GetComponent<Trailer>().GetMaxSpace();
        bool isAllowed = true;
        //Trailer.TrailerType currType;
        int currType;

        if (allowed == BOXES)
        {
            currType = trailer.GetComponent<DryVan>().GetTrailerType();
            //if (currType != Trailer.TrailerType.DRYVAN)
            if (currType != DRYVAN)
            {
                isAllowed = false;
                Debug.Log("This product is not allowed please select the correct trailer");
            }

            pay = 100 * space;
        }
        else if (allowed == COLDGOODS)
        {
            currType = trailer.GetComponent<ReferVan>().GetTrailerType();
            //if (currType != Trailer.TrailerType.REFERVAN)
            if (currType != REFERVAN)
            {
                isAllowed = false;
                Debug.Log("This product is not allowed please select the correct trailer");
            }

            pay = 100 * space;
        }
        else if (allowed == CONSTRUCTION)
        {
            currType = trailer.GetComponent<Flatbed>().GetTrailerType();
            //if (currType != Trailer.TrailerType.FLATBED)
            if (currType != FLATBED)
            {
                isAllowed = false;
                Debug.Log("This product is not allowed please select the correct trailer");
            }

            pay = 100 * space;
        }
        else if (allowed == CONSTRUCTION_LARGE)
        {
            currType = trailer.GetComponent<DropDeck>().GetTrailerType();
            //if (currType != Trailer.TrailerType.DROPDECK)
            if (currType != DROPDECK)
            {
                isAllowed = false;
                Debug.Log("This product is not allowed please select the correct trailer");
            }

            pay = 100 * space;
        }
        else if (allowed == LIQUIDS)
        {
            currType = trailer.GetComponent<Tanker>().GetTrailerType();
            //if (currType != Trailer.TrailerType.TANKER)
            if (currType != TANKER)
            {
                isAllowed = false;
                Debug.Log("This product is not allowed please select the correct trailer");
            }

            pay = 100 * space;
        }
    }
    */
    private string GenerateIcon(int productType)
    {
        string iconName = "water";

        switch (productType)
        {
            case BOXES:
                {
                    iconName = "boxes";
                    break;
                }
            case COLDGOODS:
                {
                    int randVal = Random.Range(0, 2);
                    if (randVal == 0)
                    {
                        iconName = "dairy";
                    }
                    else
                    {
                        iconName = "fruit";
                    }
                    break;
                }
            case CONSTRUCTION_LARGE:
                {
                    iconName = "ibeams";
                    break;
                }
            case CONSTRUCTION:
                {
                    iconName = "wood";
                    break;
                }
            case LIQUIDS:
                {
                    int randVal = Random.Range(0, 5);
                    if (randVal == 0)
                    {
                        iconName = "gas";
                    }
                    else if (randVal == 1)
                    {
                        iconName = "milk";
                    }
                    else if (randVal == 2)
                    {
                        iconName = "water";
                    }
                    else if (randVal == 3)
                    {
                        iconName = "oil";
                    }

                    break;
                }
        }
        return iconName;
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
        ld.productIcon = GenerateIcon(ld.productType);
        ld.DueDate = GetDeliveryTime(GameTime.inst.gmTime);
        ld.value = GetDeliveryValue(ld.productType, ld.DueDate);
        ldgo.name = ld.productLabel + " (" + ld.originLabel + " to " + ld.destinationLabel + ")";
        ldgo.transform.parent = this.transform;

        if (panelToAddLoadsTo.AddToLoadList(ldgo))
        {
            //    //loads.Add(ldgo);
        }
        else
        {
            Destroy(ldgo);
            //Debug.Log("Failed to add load to list for " + label);
        }
    }

    /*******************************************************************************
        GetDeliveryTime

        This method generates a random delivery time (days before delivery is over due)
        of between 3 and 5 days. But for sake of variety and urgency, there will be a
        10% chance for a 2 day deliver, and a 2.5% chance for a one day delivery. This 
        more rare loads will also have greater payout factors. (determined by the 
        GetDeliveryValue method.

        Delivery times are returned in int hours so we don't have to deal with odd
        minutes of the day.

    *********************************************************************************/
    private System.DateTime GetDeliveryTime(System.DateTime gtim)
    {
        System.DateTime retval = new System.DateTime(gtim.Year, gtim.Month, gtim.Day, gtim.Hour, 0, 0);

        float chnce = Random.Range(0.0f, 100.0f);
        int hrs = 0;

        if (chnce < 2.5f)                       
        {
            hrs = 12 + Random.Range(0, 24);        // up to 1.5 days delivery time
        }
        else if (chnce < 10.0)
        {
            hrs = 36 + Random.Range(0, 24);         //up to 2.5 days delivery time
        }
        else
        {
            hrs = 72 + Random.Range(0, 96);        //and this is 3 to 7 days delivery for regular price
        }

        retval = retval.AddHours(hrs);
        return (retval);

    }

    /*********************************************************************************
        GetDeliveryValue

        This method takes in the delivery time and the load type and determines 
        what the value the load (money recieved by the player upon delivery)

        Load values are determined by the Goods table, with a factor added in for 
        rushed loads (overnight deliver 3X, next day is 1.5X)

    *********************************************************************************/
    private float GetDeliveryValue(int good, System.DateTime tim)
    {
        float retval = 0.0f;

        switch (good)                               //using two digits ints for the values
        {                                           //so they appeared rounded to hundreds
            case BOXES:
                retval = 20 + Random.Range(0, 10);
                break;
            case COLDGOODS:
                retval = 25 + Random.Range(0, 15);
                break;
            case CONSTRUCTION_LARGE:
                retval = 30 + Random.Range(0, 25);
                break;
            case CONSTRUCTION:
                retval = 20 + Random.Range(0, 20);
                break;
            case LIQUIDS:
                retval = 30 + Random.Range(0, 30);
                break;
            default:
                retval = 20 + Random.Range(0, 10);
                break;
        }

        if ((tim - GameTime.inst.gmTime).TotalDays <= 1.5f)      //rushed deliveries get a price premium
        {
            retval *= 3.0f;
        }
        else if ((tim - GameTime.inst.gmTime).TotalDays <= 2.5f) 
        {
            retval *= 1.5f;
        }

        retval *= 100;                                      //now scalled to be thousands 

        return (retval);
    }


}
