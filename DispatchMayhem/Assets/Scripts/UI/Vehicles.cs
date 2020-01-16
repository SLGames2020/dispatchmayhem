using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vehicles : MonoBehaviour
{
    public GameObject MainPanel;
    private AudioSource audioSource;
    int counter;

    public void togglePanel()
    {
        counter++;
        if (counter % 2 == 1)
        {
            MainPanel.gameObject.SetActive(true);
        }
        else
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
