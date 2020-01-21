﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loads : MonoBehaviour
{
    public GameObject MainPanel;
    public Text titleText;
    public Text statusText1;
    public Text statusText2;
    private AudioSource audioSource;
    int counter;

    public void togglePanel()
    {
       // counter++;
       // if (counter % 2 == 1)
        if (!MainPanel.gameObject.activeSelf)
        {
            MainPanel.gameObject.SetActive(true);
            titleText.text = "Load Details";
            statusText1.text = "Origin\t\tDestination\t\tWeight\t\tType\t\tPay";
            statusText2.text = "Ottawa\t\tMontreal\t\t9001 lbs\t\tFood\t\t1.5/mil";
        }
        else if (MainPanel.gameObject.activeSelf && titleText.text == "Truck Details")
        {
            titleText.text = "Load Details";
            statusText1.text = "Origin\t\tDestination\t\tWeight\t\tType\t\tPay";
            statusText2.text = "Ottawa\t\tMontreal\t\t9001 lbs\t\tFood\t\t1.5/mil";
        }
        else if (MainPanel.gameObject.activeSelf && titleText.text == "Load Details")
        {
            MainPanel.gameObject.SetActive(false);
        }
    }

    public void newLoad()
    {
        if (counter >= 1)
        {
            audioSource = GetComponent<AudioSource>();
            //audioSource.PlayOneShot(SoundManager.instance.newLoad);
        }
    }
}
