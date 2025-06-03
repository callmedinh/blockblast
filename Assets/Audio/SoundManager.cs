using System;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip gameMusic;

    public void PlaySFX(Sound sound)
    {
        sound?.Play(sfxSource);
    }

    private void Awake()
    {
        PlayMusic(gameMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (musicSource.clip ==  clip) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
}
