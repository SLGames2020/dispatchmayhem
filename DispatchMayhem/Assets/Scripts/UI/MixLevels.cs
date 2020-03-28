using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetSfxlvl(float sfxLvl)
    {
        masterMixer.SetFloat("SfxVolume", sfxLvl);
    }

    public void SetMusicLvl(float musicLvl)
    {
        masterMixer.SetFloat("MusicVolume", musicLvl);
    }
}
