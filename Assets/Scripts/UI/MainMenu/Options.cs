using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour {

    public AudioMixer audioMixer;
    public void SetVolumeMaster (float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("MainMenuVolume", volume);
    }
    public void SetVolumeSFX(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }
}