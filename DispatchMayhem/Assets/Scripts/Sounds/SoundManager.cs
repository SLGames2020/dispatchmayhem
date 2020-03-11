using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource mainMusic;
    public AudioSource soundEffects;
    public AudioSource soundEffects1;
    public AudioSource truckSounds;

    public static SoundManager instance = null;
   
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
   
    public void MainMusic(AudioClip clip)
    {
        mainMusic.clip = clip;
        mainMusic.Play();

    }

    public void SoundEffects(AudioClip clip)
    {
        soundEffects.clip = clip;
        soundEffects.PlayOneShot(clip, 0.7f);

        soundEffects1.clip = clip;
        soundEffects1.PlayOneShot(clip, 0.7f);
    }


    public void TruckSounds(AudioClip clip)
    {
        truckSounds.clip = clip;
        truckSounds.PlayOneShot(clip, 0.8f);
    }
}
