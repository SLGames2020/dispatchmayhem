using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    public Vector2 origin;
    public Vector2 destination;
    public routeCallBack FoundRoute;
    public List<Vector2> route;                                                     // Mapbox route from Origin to Destination; can be used by truck/movement script to reduce route calls
    public float routeDistance;

    public string originLabel;                                                      // where to pickup load
    public string destinationLabel;                                                 // where to drop off load
    public string productLabel;
    public string productIcon;

    public float value = 0.0f;                                                      // cost gained or lost from successfully delivering or failing to deliver in time. (this is set in the callback for getting the route)
    public float haulingCost = 1.0f;                                                // The cost of the driver and general operation of the truck per mile (pay, fuel, general maitenance)
    public float haulingPayment = 2.5f;                                             // The amount of money per mile earned from transportint the load (determined by the load type)
    public System.DateTime DueDate;                                                 // when the load needs to be dropped off by.
    public int productType;
    //public int typeIDX; // type of load i.e (OIL, WATER, IBEAMS, CORN etc...)

    public bool assigned = false;
    public int driverID = -1; // unassigned

    public enum LoadState
    {
        UNASSIGNED,
        ASSIGNED,
        DELIVERING,
        DELIVERED,
        PAID,

        NUM_LOAD_STATES
    }

    public LoadState state;

    void Start()
    {
        FoundRoute = RouteCallBack;
        route.Clear();
    }

    public Load()
    {
        state = LoadState.UNASSIGNED;
    }

    /**************************************************************
        FindRoute

        This method is used to find the route between the origin
        and the destination of this load. Primarily this is to 
        get the distance of the route; but the route is saved
        so that the truck/movement script can use it too
        (instead of making a second route call)

        This is mainly used by the load manager, as the callback
        does not want to work when delegated from there

    ***************************************************************/
    public void FindRoute()
    {
        //NM.Inst.GetRoute(origin, destination, FoundRoute);
        //creating a very rough distance calculation for now as my callback is not working
        float tmpdst = Mathf.Abs((destination.y - origin.y) * 69.171f);  
        tmpdst += Mathf.Abs((destination.x - origin.x) * 69.171f * Mathf.Cos(origin.y * Mathf.PI / 180.0f));
        routeDistance = tmpdst;
        value = haulingPayment * tmpdst;
        //Debug.Log("Load(" + originLabel + " - " + destinationLabel + ") Valued at: $" + value + " for " + tmpdst + " miles");
    }

    /**************************************************************
        RouteCallBack

        This is the call back for when the route has been recieved
        by the Network Manager (through the mapbox api)

    ***************************************************************/
    public void RouteCallBack(List<Vector2> rte, float dst)
    {
        foreach (Vector2 pnt in rte)
        {
            route.Add(pnt);
        }
        routeDistance = dst / 1609.34f;                              //convert mapbox distance in meters to miles
        value = haulingPayment * dst;
        Debug.Log("Load (" + originLabel + " - " + destinationLabel + "): " + route.Count + " Waypoints, Valued at: $" + value + " for " + dst + " miles");
    }
}
