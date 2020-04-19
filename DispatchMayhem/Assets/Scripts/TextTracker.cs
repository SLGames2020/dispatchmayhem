using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextTracker : MonoBehaviour
{
    public Text nameLabel;
    public Vector3 posShift = new Vector3(0.0f, 0.0f, 0.0f);    //offset the text from the objects
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 namepos = Camera.main.WorldToScreenPoint(this.transform.position);
        namepos += posShift;
        nameLabel.transform.position = namepos;
    }
}
