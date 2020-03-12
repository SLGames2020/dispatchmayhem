using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource mainMusic;
    public AudioSource shop;
    public AudioSource drivers;
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

    public void Shop(AudioClip clip)
    {
        shop.clip = clip;
        shop.PlayOneShot(clip, 0.6f);

    }

    public void Drivers(AudioClip clip)
    {
        drivers.clip = clip;
        drivers.PlayOneShot(clip, 0.7f);
    }

    public void TruckSounds(AudioClip clip)
    {
        truckSounds.clip = clip;
        truckSounds.PlayOneShot(clip, 0.8f);
    }
}
