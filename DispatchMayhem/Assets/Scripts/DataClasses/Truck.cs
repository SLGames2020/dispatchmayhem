using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    public enum TruckStates { IDLE, LOADING, ONROUTE, UNLOADING, IMPOUNDED, REPAIRING }

    public enum TruckTypes { DRYVAN, REEFER, STEPDECK, FLATBED, CHEMICAL, FOODGRADE }

    public TruckTypes truckType = TruckTypes.DRYVAN;           //See LoadM for the list of load types supported
    [HideInInspector] public int productType;               //named to match the load productType (these must equal to accept a load)
    [HideInInspector] public string rigLabel = "Undefined transport";

    public TruckStates status = TruckStates.IDLE;

    public int xp;
    public int wage;
    public int level;

    void Start()
    {    
        switch (truckType)
        {
            case TruckTypes.DRYVAN:
                productType = 0;
                break;
            case TruckTypes.FOODGRADE:
                productType = 1;
                break;
            case TruckTypes.FLATBED:
                productType = 2;
                break;
            case TruckTypes.REEFER:
                productType = 3;
                break;
            case TruckTypes.STEPDECK:
                productType = 4;
                break;
            case TruckTypes.CHEMICAL:
                productType = 5;
                break;
        }
        rigLabel = LoadM.productLabels[productType] + " Truck";
    }
}