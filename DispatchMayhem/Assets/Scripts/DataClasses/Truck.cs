using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    public enum TruckStates { IDLE, LOADING, ONROUTE, UNLOADING, IMPOUNDED, REPAIRING }

    // public LoadType type;
    public LoadManager.Products type;
    public string rigLabel = "Undefined transport";

    public TruckStates status = TruckStates.IDLE;

    TruckType[] SemiTruck =
    {
         new TruckType(0, 500, 0, 0, 115, 125000.00f, 500.00f),
         new TruckType(0, 550, 0, 0, 115, 150000.00f, 600.00f),
         new TruckType(0, 600, 0, 0, 115, 175000.00f, 700.00f),
         new TruckType(0, 650, 0, 0, 115, 200000.00f, 800.00f),
         new TruckType(0, 700, 0, 0, 115, 225000.00f, 900.00f),
         new TruckType(0, 750, 0, 0, 115, 250000.00f, 1000.00f),
    };

    TruckType[] BoxTruck =
    {

            new TruckType(0, 200, 0, 200, 70, 25000.00f, 100.00f),
            new TruckType(0, 250, 0, 250, 80, 50000.00f, 200.00f),
            new TruckType(0, 300, 0, 300, 90, 75000.00f, 300.00f),
            new TruckType(0, 350, 0, 350, 100, 100000.00f, 400.00f),
            new TruckType(0, 400, 0, 400, 110, 125000.00f, 450.00f),
            new TruckType(0, 450, 0, 450, 120, 150000.00f, 500.00f),
    };

    public void Loading()
    {

    }

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
