using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reputation : MonoBehaviour
{
    public Image Heart;


    void Rep()
    {
        Movement loadjr = GM.inst.GetComponent<Movement>();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            float movement = 1f;
            float horizontalInput = Input.GetAxis("Horizontal");

            Heart.transform.position = transform.position + new Vector3(horizontalInput * movement, 0);
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
