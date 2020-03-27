using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckBuy : MonoBehaviour
{
    
    public void BuyUlitmateTruck()
    {
        Finances.inst.ValidatePurchase(250000f);
    }
    
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
