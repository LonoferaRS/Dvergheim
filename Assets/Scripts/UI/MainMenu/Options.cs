using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour 
{
    public SoundManager music_guitar_script;
    public Toggle Death_toggle;
    public Toggle Music_Off;
    public Slider Sfx_Slider;
    public Slider Music_Slider;
    public void Start()
    {
        if (PlayerPrefs.GetInt("SfxDeath") == 0)
            Death_toggle.isOn = false;
        else
            Death_toggle.isOn = true;
        Sfx_Slider.value = PlayerPrefs.GetFloat("SfxVolume");
        Music_Slider.value = PlayerPrefs.GetFloat("MusicVolume");

        if (PlayerPrefs.GetInt("MusicOff") == 0)
            Music_Off.isOn = false;
        else
           Music_Off.isOn = true;
    }
    public void SetVolumeMusic(float volume)
    {
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }
    public void SetVolumeSFX(float volume)
    {
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }
    public void SetVolumeDeath(bool volume)
    {
        
        if (volume)
        {
            PlayerPrefs.SetInt("SfxDeath", 1);
        }
        else
            PlayerPrefs.SetInt("SfxDeath", 0);
    }
    public void SetVolumeMusicOff(bool volume)
    {
        if (volume)
        {
            PlayerPrefs.SetInt("MusicOff", 1);
        }
        else
            PlayerPrefs.SetInt("MusicOff", 0);
        music_guitar_script.UpdateButtonImage();
    }
    public void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}