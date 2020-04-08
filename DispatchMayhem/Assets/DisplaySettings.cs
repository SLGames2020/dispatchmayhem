using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySettings : MonoBehaviour
{
    public GameObject loadButton;
    public GameObject saveButton;
        
    //start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    //void Update()
    //{

    void OnEnable()
    {
        Debug.Log("Settings Enabled");
        if (GM.inst.haveSave)
        {
            Debug.Log("And has save");
            loadButton.GetComponent<Image>().sprite = saveButton.GetComponent<Image>().sprite;
        }
    }


}
