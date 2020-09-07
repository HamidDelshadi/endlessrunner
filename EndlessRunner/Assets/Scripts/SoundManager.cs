using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//types of sound effects
public enum SFX{bg=0, magic=1,dying=2,explosion=3,footstep1=4, footstep2=5,jump=6, pickup=7 }

public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// this classs is responsible for playing sound effects and music
    /// </summary>

    public static SoundManager Instance = null; //singletone
    private AudioSource[] SFXs; // the list of sound effects
    private void Awake()
    {
        if (Instance) //singleton pattern in unity
            Destroy(this.gameObject);
        else
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
    }
    void Start()
    {
        SFXs = GetComponentsInChildren<AudioSource>();
    }

    public void PlaySFX(SFX sfx)
    {
        //plays a sound effect
        SFXs[(int)sfx].Play();
    }

    public void UpdateMusicVolume(float value)
    {
        //changes the music volume
        PlayerPrefs.SetFloat("musicvolume", value);
        SFXs[(int)SFX.bg].volume = value;
    }

    internal void UpdateSFXVolume(float value)
    {
        //changes the sound effects volume
        PlayerPrefs.SetFloat("soundvolume", value);
        for (int i = 0; i < SFXs.Length; i++)
            if (i != (int)SFX.bg)
                SFXs[i].volume = value;
    }
}
