using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickSound : MonoBehaviour
{
    public AudioSource sound;

    public AudioClip click;

    public void Clicksound()
    {
        sound.PlayOneShot(click);
    }
}
