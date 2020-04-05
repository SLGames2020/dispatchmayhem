using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    public Vector2 origin;
    public Vector2 destination;

    public string originLabel;                                                      // where to pickup load
    public string destinationLabel;                                                 // where to drop off load
    public string productLabel;
    public string productIcon;

    public float value;                                                             // cost gained or lost from successfully delivering or failing to deliver in time.
    public float haulingCost = 1.5f;
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

    public Load()
    {
        state = LoadState.UNASSIGNED;
    }
}
