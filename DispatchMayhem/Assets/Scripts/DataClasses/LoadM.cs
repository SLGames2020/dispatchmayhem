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
    private const int VAN = 0;                  //using const ints because Enums are difficult to use randomly
    private const int REEFER = 1;
    private const int STEPDECK = 2;
    private const int FLATBED = 3;
    private const int CHEMICAL = 4;
    private const int FOODGRADE = 5;
    private const int UNDEFINED = 6;             //error catch, only ever set at instantiation (always equal to TOTALPRODUCTS)
    private const int TOTALPRODUCTS = 6;
    //}



    public int allowed;
    //make sure to add new product labels before the "Undefined" entry
    private static string[] productLabels = { "Van", "Reefer", "Step-Deck", "Flatbed", "Chemical", "Food Grade", "Undefined" };
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

    public void AllowedLoads()
    {

    }


    private string GenerateIcon(int productType)
    {
        string iconName = "water";

        switch (productType)
        {
            case VAN:
                {
                    iconName = "boxes";
                    break;
                }
            case REEFER:
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
            case STEPDECK:
                {
                    iconName = "ibeams";
                    break;
                }
            case FLATBED:
                {
                    iconName = "wood";
                    break;
                }
            case CHEMICAL:
                {
                    int randVal = Random.Range(0, 2);
                    if (randVal == 0)
                    {
                        iconName = "gas";
                    }
                    else if (randVal == 1)
                    {
                        iconName = "oil";
                    }
                    break;
                }
            case FOODGRADE:
                {
                    int randVal = Random.Range(0, 2);
                    if (randVal == 0)
                    {
                        iconName = "milk";
                    }
                    else if (randVal == 1)
                    {
                        iconName = "oil";
                    }
                    break;
                }
            default:
                {
                    iconName = "undefined";
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
        Vector2 desgps;
        GameObject go;

        do
        {
            go = CyM.inst.openCities[Random.Range(0, CyM.inst.openCities.Count - 1)];
            desgps = go.GetComponent<MapSupport>().gps;
        }
        while (desgps == orgps);                     //loop till we find a city that isn't the calling city

        GameObject ldgo = Instantiate(loadPrefab, this.transform.position, Quaternion.identity);
        Load ld = ldgo.GetComponent<Load>();
        ld.originLabel = orlbl;
        ld.origin = orgps;
        ld.destinationLabel = go.GetComponent<City>().label;
        ld.destination = desgps;
        ld.productType = Random.Range(0, TOTALPRODUCTS);
        ld.productLabel = productLabels[ld.productType];
        ld.productIcon = GenerateIcon(ld.productType);
        ld.DueDate = GetDeliveryTime(GameTime.inst.gmTime);
        ld.haulingPayment = GetDeliveryValue(ld.productType, ld.DueDate);
        ld.FindRoute();
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
        of between 1.5 and 2.5 days. But for sake of variety and urgency, there will be a
        7.5% chance 3+ day deliveries, and a 2.5% chance for a one day delivery. 
        (see GetDeliveryValue for the pay scales)

        Delivery times are returned in int hours so we don't have to deal with odd
        minutes of the day.

    *********************************************************************************/
    private System.DateTime GetDeliveryTime(System.DateTime gtim)
    {
        System.DateTime retval = new System.DateTime(gtim.Year, gtim.Month, gtim.Day, gtim.Hour, 0, 0);

        float chnce = Random.Range(0.0f, 100.0f);
        int hrs = 0;

        if (chnce < 2.5f)                          //2.5% chance of a rush delivery
        {
            hrs = 12 + Random.Range(0, 24);        // up to 1.5 days delivery time
        }
        else if (chnce < 90.0)                      //90% chance for the normal stuff
        {
            hrs = 36 + Random.Range(0, 24);         //up to 2.5 days delivery time
        }
        else                                        //7.5% chance for "intermodal" deliveries
        {
            hrs = 72 + Random.Range(0, 96);        //and this is 3 to 7 days delivery for reduced rate
        }

        retval = retval.AddHours(hrs);
        return (retval);

    }

    /*********************************************************************************
        GetDeliveryValue

        This method takes in the delivery time and the load type and determines 
        what the value per mile is of delivering the load

        Load values are determined by the Goods table, with a factor added in for 
        rushed loads: overnight is 3X, next day is standard, and long delivery pays less

    *********************************************************************************/
    private float GetDeliveryValue(int good, System.DateTime tim)
    {
        float retval = 0.0f;

        switch (good)                               //using two digits ints for the values
        {                                           //so they appeared rounded to hundreds
            case VAN:
                retval = Random.Range(1.5f, 2.10f);
                break;
            case REEFER:
                retval = Random.Range(1.85f, 2.25f);
                break;
            case STEPDECK:
                retval = Random.Range(2.50f, 3.50f);
                break;
            case FLATBED:
                retval = Random.Range(2.30f, 3.00f);
                break;
            case CHEMICAL:
            case FOODGRADE:
                retval = Random.Range(2.50f, 3.50f);
                break;
            default:
                retval = Random.Range(1.50f, 3.50f);
                break;
        }

        if ((tim - GameTime.inst.gmTime).TotalDays <= 1.5f)      //rushed deliveries get a price premium
        {
            retval *= 3.0f;
        }
        else if ((tim - GameTime.inst.gmTime).TotalDays <= 2.5f) //Normal window for loads
        {
            retval *= 1.0f;
        }
        else                                                    //"Intermodal" delivers pay less because of the
        {                                                       //longer time allotment (3 to 7 days)
            retval *= 0.85f;
        }

        return (retval);
    }


}

