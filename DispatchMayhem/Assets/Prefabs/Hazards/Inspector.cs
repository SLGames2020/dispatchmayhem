using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inspector : MonoBehaviour
{
    public Text inspctTimeText;

    public float lifespan = 12.0f;

    private DateTime inspctEnd;
    private int lastHour;


    // Start is called before the first frame update
    void Start()
    {
        lastHour = GameTime.inst.gmHour;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameTime.inst.gmHour != lastHour)
        {
            lastHour = GameTime.inst.gmHour;
            lifespan -= 1.0f;
            if (lifespan <= 0.0f)
            {
                HazM.Inst.CloseInspectionStation(this.gameObject);
            }
        }
        if (inspctTimeText.enabled)                                 //if we're inspecting a truck, show the player the time remaining
        {
            TimeSpan timleft = inspctEnd - GameTime.inst.gmTime;
            if (timleft < TimeSpan.Zero)
            {
                inspctTimeText.enabled = false;
            }
            else
            {                
                inspctTimeText.text = string.Format("{0:00}:{1:00}", timleft.Hours, timleft.Minutes);
            }
        }
    }
    /*********************************************************************
        OnTriggerEnter 

        This event is used to set a wait time for when a truck hits our
        inspection object. It will also show a countdown for how much
        time to wait for the ispection to finish

    **********************************************************************/
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Truck")
        {
            float wtime = HazM.Inst.GetInspectionTime();
            col.gameObject.GetComponent<Movement>().SetWaitTime(wtime);

            inspctEnd = GameTime.inst.gmTime;         
            inspctEnd = inspctEnd.AddHours(wtime);
            inspctTimeText.enabled = true;
            if (lifespan < wtime )                  //Don't let the inspection station close until                 
            {                                       //it's last inspection has completed
                lifespan = wtime;
            }
        }
    }
}
