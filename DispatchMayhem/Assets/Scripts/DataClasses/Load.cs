using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    public Vector2 origin;
    public Vector2 destination;

    public string originLabel; // where to pickup load
    public string destinationLabel; // where to drop off load

    public float value; // cost gained or lost from successfully delivering or failing to deliver in time.
    public System.DateTime DueDate; // when the load needs to be dropped off by.
    public int typeIDX; // type of load i.e (OIL, WATER, IBEAMS, CORN etc...)

    public float haulingCost = 1.5f;

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

    public void GenerateGoodsAndPrices()
    {
        typeIDX = Random.Range(0, 2); // create an enum later for all the types of load contents

        double tmpDueInc = Random.Range(0.1000f, 6.600f);
        DueDate = System.DateTime.Now.Add(System.TimeSpan.FromDays( tmpDueInc));

        //JD TODO: in the future make load contents and due date from current time scale the price
        value = Random.Range(100, 10000);
    }
}
