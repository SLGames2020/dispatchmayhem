using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource mainMusic;
    public AudioSource warningSound;
    public AudioSource truckStart;
    public AudioSource truckBackingup;
    public AudioSource truckIdling;
    public AudioSource truckMoving;

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

    public void Warning(AudioClip clip)
    {
        warningSound.clip = clip;
        warningSound.PlayOneShot(clip, 1.0f);

    }

    public void TruckStart(AudioClip clip)
    {
        truckStart.clip = clip;
        truckStart.PlayOneShot(clip, 0.6f);
    }

    public void TruckBackingUp(AudioClip clip)
    {
        truckBackingup.clip = clip;
        truckBackingup.PlayOneShot(clip, 0.6f);
    }

    public void TruckIdle(AudioClip clip)
    {
        truckIdling.clip = clip;
        truckIdling.PlayOneShot(clip, 0.6f);
    }

    public void TruckMoving(AudioClip clip)
    {
        truckMoving.clip = clip;
        truckMoving.PlayOneShot(clip, 0.6f);
    }

}
