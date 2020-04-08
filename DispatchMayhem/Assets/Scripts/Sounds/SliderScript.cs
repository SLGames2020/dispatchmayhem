using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public AudioMixer mixer;

    public string mixerName;

    private void Start()
    {
        float tmpvol = 0.0f;

        if (PlayerPrefs.HasKey(mixerName))
        {
            Debug.Log("loading Volume: " + mixerName);
            tmpvol = PlayerPrefs.GetFloat(mixerName);
            mixer.SetFloat(mixerName, Mathf.Log10(tmpvol) * 20);
            this.gameObject.GetComponent<Slider>().value = tmpvol;
        }
    }

    public void SetLevel(float sliderValue)
    {
        Debug.Log("saving volume: " + mixerName);
        mixer.SetFloat(mixerName, Mathf.Log10 (sliderValue) * 20);
        PlayerPrefs.SetFloat(mixerName, sliderValue);
    }

}
