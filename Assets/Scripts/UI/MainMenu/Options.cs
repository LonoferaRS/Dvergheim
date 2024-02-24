using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour {

    public void SetVolumeMusic(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetVolumeSFX(float volume)
    {
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }
}