
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageBoxCtrl : MonoBehaviour
{
    public Text titleTextObj;
    public Text typeTextObj;
    public Text messageTextObj;
    public TextMeshProUGUI buttonTextObj;

    public string title = "Title";
    public string topic = "Topic";
    public string message = "My message";
    public string buttonTxt = "Button";

    // Start is called before the first frame update
    void Start()
    {
        titleTextObj.text = title;
        typeTextObj.text = topic;
        messageTextObj.text = message;
        buttonTextObj.text = buttonTxt;
        //Debug.Log(title + " " + topic + " " + message + " " + buttonTxt);
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    /*******************************************************
        CloseMessage
        Hides the message panel when the button is pressed
    *********************************************************/
    public void CloseMessage()
    {
        this.gameObject.SetActive(false);
    }

}