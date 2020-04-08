using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    //TODO: 1) The saving of the volume settings (and game settings in general) should have
    //         a seperate button so its understood to not be part of the Save Game button
    //      2) The setting of the mixer and saving/retrieving of the values should be moved 
    //         to the sound manager

    public AudioMixer mixer;

    public string mixerName;

    private void Start()
    {
        float tmpvol = 0.0f;

        if (PlayerPrefs.HasKey(mixerName))
        {
            tmpvol = PlayerPrefs.GetFloat(mixerName);
            this.gameObject.GetComponent<Slider>().value = tmpvol;
        }
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(mixerName, Mathf.Log10 (sliderValue) * 20);
        PlayerPrefs.SetFloat(mixerName, sliderValue);
    }
}
