using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public AudioClip buttonClick;
    public AudioClip bgSound;
    public AudioSource uiAudioSource;
    public AudioSource bgAudioSource;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlaySound(bgAudioSource,bgSound);
    }

    public void PlaySound(AudioSource audioSource,AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
