using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSetting : MonoBehaviour
{
    /// <summary>
    /// it manages the music and sound settings in the main menu
    /// </summary>
    public Slider MusicSlider;
    public Slider SoundSlider;
    void Start()
    {
        InitiateSlider(MusicSlider, "musicvolume"); //initiate the volume
        InitiateSlider(SoundSlider, "soundvolume");
        SoundManager.Instance.UpdateMusicVolume(MusicSlider.value); //updates the volume
        SoundManager.Instance.UpdateSFXVolume(SoundSlider.value);
    }
    void InitiateSlider(Slider slider, string tag)
    {
        //loads saved volume settings from playerprefs and sets the value of the sliders
        if (PlayerPrefs.HasKey(tag))
        {
            slider.value = PlayerPrefs.GetFloat(tag);
        }
        else
        {
            slider.value = 1;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMusicVolume(float value)
    {
        SoundManager.Instance.UpdateMusicVolume(value);
    }

    public void SFXMusicVolume(float value)
    {
        SoundManager.Instance.UpdateSFXVolume(value);
    }
}
