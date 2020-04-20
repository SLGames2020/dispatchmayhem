using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruckBuy : MonoBehaviour
{
    public ValidPurchaseCallBack truckBought;

    public Button foodGradeButton;
    public Button flatBedButton;
    public Button reeferButton;

    private int truckBoughtID = 0;

    void Start()
    {
        truckBought = BuyTruckCallBack;
    }

    public void BuyUlitmateTruck()
    {
        truckBoughtID = 1;          
        Finances.inst.ValidatePurchase(100000f, "FoodGrade", truckBought);
    }

    public void BuyFlatbedTruck()
    {
        truckBoughtID = 2;
        Finances.inst.ValidatePurchase(100000f, "FlatBed", truckBought);
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
            switch(truckBoughtID)
            {
                case 1: foodGradeButton.interactable = false;
                        break;
                case 2: flatBedButton.interactable = false;
                        break;
                case 3: reeferButton.interactable = false;
                        break;
            }
        }
    }
}
