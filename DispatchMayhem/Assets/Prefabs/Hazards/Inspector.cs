using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspector : MonoBehaviour
{

    public float lifespan = 12.0f;

    private int lastHour;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Inspector Started");
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
    }
    //called on contact with another collider or rigid body
    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Inspector Collision Detected");
        if (col.tag == "Truck")
        {
            float wtime = HazM.Inst.GetInspectionTime();
            col.gameObject.GetComponent<Movement>().SetWaitTime(wtime);

            if(lifespan < wtime )                   //Don't let the inspection station close until                 
            {                                       //it's last inspection has completed
                lifespan = wtime;
            }
            Debug.Log("Inspection Time: " + wtime);
        }
    }
}
