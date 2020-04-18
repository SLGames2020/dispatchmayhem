﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reputation : MonoBehaviour
{
    public Image Heart;


    void Rep()
    {
        Load repLoad = GM.inst.GetComponent<Load>();
        if (repLoad != null)
        {
            Debug.Log("repLoad: " + repLoad.state);
            if (repLoad.state == Load.LoadState.DELIVERED)
            {
                float movement = 10f;
                float horizontalInput = 1.0f;

                Heart.transform.localPosition = transform.localPosition + new Vector3(horizontalInput * movement, 0);
            }
            else if (repLoad.state == Load.LoadState.UNASSIGNED)
            {
                float movement = -10f;
                float horizontalInput = 1.0f;

                Heart.transform.localPosition = transform.localPosition + new Vector3(horizontalInput * movement, 0);
            }
        }

    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Rep();
    }
}
