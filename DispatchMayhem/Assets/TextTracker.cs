using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextTracker : MonoBehaviour
{
    public Text nameLabel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 namepos = Camera.main.WorldToScreenPoint(this.transform.position);
        //namepos.z = 1.0f;
        nameLabel.transform.position = namepos;
    }
}
