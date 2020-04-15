﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckBuy : MonoBehaviour
{
    public ValidPurchaseCallBack truckBought;

    private int truckBoughtID = 0;

    void Start()
    {
        truckBought = BuyTruckCallBack;
    }

    public void BuyUlitmateTruck()
    {
        truckBoughtID = 1;          
        Finances.inst.ValidatePurchase(250000f, "Ultimate Truck", truckBought);
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void BuyTruckCallBack(bool yes)
    {
        if (yes)
        {
            GM.inst.ActivateTrucknDriver(truckBoughtID);
        }
    }
}
