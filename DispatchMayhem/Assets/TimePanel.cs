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
       if (PlayerPrefs.HasKey("TimeButton"))
        {
            int buttonID = PlayerPrefs.GetInt("TimeButton");
            gameObject.GetComponent<Image>().sprite = ButtonImages[buttonID];
            GameTime.inst.timeScale = (buttonID * buttonID * 5.0f) + 1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTime(int buttonID)
    {
        // swap the image on the bar when selected.
        gameObject.GetComponent<Image>().sprite = ButtonImages[buttonID];

        // update the time manager to process at new speed.
        GameTime.inst.timeScale = (buttonID * buttonID * 5.0f) + 1.0f;

        PlayerPrefs.SetInt("TimeButton", buttonID);
    }
}
