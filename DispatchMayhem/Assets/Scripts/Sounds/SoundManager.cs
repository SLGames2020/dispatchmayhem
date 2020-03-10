using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource mainMusic;
    public AudioSource soundEffects;

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
    }
}
