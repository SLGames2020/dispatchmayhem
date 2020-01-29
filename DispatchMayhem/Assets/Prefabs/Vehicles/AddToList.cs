using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToList : MonoBehaviour
{

    public GameObject listItemTemplate;
    public GameObject loadBoxContent;
    [HideInInspector] public GameObject goToAdd;

    // Start is called before the first frame update
    /*void Start()
    {
        
    }*/

    // Update is called once per frame
    /*void Update()
    {
        
    }*/


    /****************************************************************************
        AddLoadToLoadBox

    *****************************************************************************/
    private bool AddLoadToLoadBox(Load ld)
    {
        string txt = ld.originLabel + " to " + ld.destinationLabel;
        var copy = Instantiate(listItemTemplate);
        copy.transform.parent = loadBoxContent.transform;
 //       copy.GetComponentInChildren<Text>().text = txt;
        return true;
    }


}
