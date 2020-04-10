using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioSource mainMusic;
    public AudioSource soundEffects;
    public AudioMixer mixer;

    public static SoundManager instance = null;

    public string[] mixers = { "MainMusicVol", "SoundFxVol" };

    private void Awake()
    {

       if (instance == null)
       {
            instance = this;
       }
       else if (instance != this)
       {
            Destroy(gameObject);
       }

       DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        float tmpvol = 0.0f;

        foreach (string mixname in mixers)
        {
            if (PlayerPrefs.HasKey(mixname))
            {
                tmpvol = PlayerPrefs.GetFloat(mixname);
                //Debug.Log("setting: " + mixname + "  Volume to: " + tmpvol);
                mixer.SetFloat(mixname, Mathf.Log10(tmpvol) * 20);
            }
        }
    }
   
    public void MainMusic(AudioClip clip)
    {
        mainMusic.clip = clip;
        mainMusic.Play();

    }

    public void SoundEffect(AudioClip clip)
    {
        soundEffects.clip = clip;
        soundEffects.PlayOneShot(clip, 1.0f);

    }

}
