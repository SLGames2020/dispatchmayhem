using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reputation : MonoBehaviour
{
    public Image Heart;

    bool GoodDelivevery;

    public void Rep(bool GoodDelievery)
    {
        
            if (GoodDelievery == true)
            {
                float movement = 10f;
                float horizontalInput = 1.0f;
                Vector3 maxposition = new Vector3(-343.5f,  0f, 0);
                if (Heart.transform.localPosition != maxposition)
                {
                    Heart.transform.localPosition = transform.localPosition + new Vector3(horizontalInput * movement, 0);
                }
            }
            else if (GoodDelievery == false)
            {
                float movement = -10f;
                float horizontalInput = 1.0f;
                Vector3 maxposition = new Vector3(-743.5f, 0f, 0);

                if (Heart.transform.localPosition != maxposition)
                {
                    Heart.transform.localPosition = transform.localPosition + new Vector3(horizontalInput * movement, 0);
                }
          
            }

    }


    // Start is called before the first frame update
   /* void Start()
    {
        Rep(GoodDelivevery);
        Debug.Log("GoodDelivevery: " + GoodDelivevery);
        Debug.Log("CurrenPosition: " + Heart.transform.localPosition.x);
    }

    // Update is called once per frame
    void Update()
    {
     
    }*/
}
