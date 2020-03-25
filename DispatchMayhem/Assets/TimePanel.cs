using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePanel : MonoBehaviour
{
    public Sprite[] ButtonImages;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTime(int buttonID)
    {
        Debug.Log("hanging Time");
        // swap the image on the bar when selected.
        gameObject.GetComponent<Image>().sprite = ButtonImages[buttonID];

        // update the time manager to process at new speed.
        GameTime.inst.timeScale = (buttonID+1) * 1.0f;
    }
}
