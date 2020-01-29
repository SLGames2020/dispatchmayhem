using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip mainMusic;
    public AudioClip truckArrives;
    public AudioClip truckLeaves;
    public AudioClip scaleStation;
    public AudioClip issueOccurs;
    public AudioClip fined;
    public AudioClip newLoad;
    public AudioClip buttonClicks;
    public static SoundManager instance = null;
    private AudioSource soundEffectAudio;

    private void Start()
    {

       if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach(AudioSource source in sources)
        {
            if(source.clip == null)
            {
                soundEffectAudio = source;
            }
        }

        DontDestroyOnLoad(gameObject);
    }
    public void PlayOneShot(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);
    }
}
