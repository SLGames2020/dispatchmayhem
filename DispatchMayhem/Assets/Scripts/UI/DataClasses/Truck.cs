using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    public enum TruckStates { IDLE, LOADING, ONROUTE, UNLOADING, IMPOUNDED, REPAIRING }

    public LoadType type;
    public string rigLabel = "Undefined transport";

    public TruckStates status = TruckStates.IDLE;

    // Start is called before the first frame update
    //void Start()
    //{
    //    
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    
    //}
}
