using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MainMusicVol", Mathf.Log10 (sliderValue) * 20);
    }

    public void SetLevel2(float sliderValue2)
    {
        mixer.SetFloat("SoundFxVol", Mathf.Log10 (sliderValue2) * 20);
    }
}
