using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioSource clickSource;
    [SerializeField] private AudioSource winSource;
    [SerializeField] private AudioSource loseSource;
    [SerializeField] private AudioSource rouletteSource;

    public static AudioControl Instance { get; private set; }

    private void Start()
    {
        Instance = this;
    }

    public bool SoundIsOn
    {
        get => musicSource.isPlaying;
        set
        {
            if(value) musicSource.Play();
            else musicSource.Stop();
        }
    }

    public void Click()
    {
        if(SoundIsOn) clickSource.Play();
    }

    public void Right()
    {
        if(SoundIsOn) winSource.Play();
    }

    public void Fail()
    {
        if(SoundIsOn) loseSource.Play();
    }

    public void RouletteLaunch()
    {
        if(SoundIsOn) rouletteSource.Play();
    }

    public void StopRoulette()
    {
        if (SoundIsOn) rouletteSource.Stop();
    }
}
