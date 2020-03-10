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
        lastHour = GameTime.Inst.gmHour;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameTime.Inst.gmHour != lastHour)
        {
            lastHour = GameTime.Inst.gmHour;
            lifespan -= 1.0f;
            if (lifespan <= 0.0f)
            {
                HazM.Inst.CloseInspectionStation(this.gameObject);
            }
        }
    }
    //called on contact with another collider or rigid body
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Truck")
        {
            float wtime = HazM.Inst.GetInspectionTime();
            col.gameObject.GetComponent<Movement>().SetWaitTime(wtime);
            if(lifespan < wtime )
            {
                lifespan = wtime;
            }
            Debug.Log("Inspection Time: " + wtime);
        }
    }
}
