using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehiclesUI : MonoBehaviour
{
    public GameObject MainPanel;
    public Text titleText;
    public Text statusText1;
    public Text statusText2;
    private AudioSource audioSource;
    int counter;

    public void togglePanel()
    {
        //counter++;
        if (!MainPanel.gameObject.activeSelf)
        {
            MainPanel.gameObject.SetActive(true);
            titleText.text = "Truck Details";
            statusText1.text = "";
            statusText2.text = "";
        }
        else if (MainPanel.gameObject.activeSelf && titleText.text == "Load Details")
        {
            titleText.text = "Truck Details";
            statusText1.text = "";
            statusText2.text = "";
        }
        else if (MainPanel.gameObject.activeSelf && titleText.text == "Truck Details")
        {
            MainPanel.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.PlayOneShot(SoundManager.instance.truckArrives);
        //audioSource.PlayOneShot(SoundManager.instance.truckLeaves);
    }
}
