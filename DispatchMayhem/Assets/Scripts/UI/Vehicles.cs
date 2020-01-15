using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vehicles : MonoBehaviour
{
    public GameObject SidePanel;
    private AudioSource audioSource;
    int counter;

    public void togglePanel()
    {
        counter++;
        if (counter % 2 == 1)
        {
            SidePanel.gameObject.SetActive(true);
        }
        else
        {
            SidePanel.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.PlayOneShot(SoundManager.instance.truckArrives);
        //audioSource.PlayOneShot(SoundManager.instance.truckLeaves);
    }
}
