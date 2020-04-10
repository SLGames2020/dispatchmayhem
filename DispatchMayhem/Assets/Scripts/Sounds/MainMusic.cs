using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusic : MonoBehaviour
{

    public AudioClip mainMusic;

    void Start()
    {
        SoundManager.instance.MainMusic(mainMusic);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
