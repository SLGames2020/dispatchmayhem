using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    public enum TruckStates { IDLE, LOADING, ONROUTE, UNLOADING, IMPOUNDED, REPAIRING }

    //public LoadType type; // this is breaking the build... we don't have a loadtype class...
    public string rigLabel = "Undefined transport";

    public TruckStates status = TruckStates.IDLE;

    public int xp;
    public int wage;
    public int level;


    public void Loading()
    {

    }

}