using System.Collections.Generic;
using Roots;
using UnityEngine;

namespace Audio
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioProfile[] musicProfiles;
        Dictionary<State, AudioProfile> _audioMap = new();

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            foreach (var musicProfile in musicProfiles)
            {
                _audioMap[musicProfile.state] = musicProfile;
            }
        }

        public void Play(State state)
        {
            if (_audioMap.TryGetValue(state, out var profile))
            {
                if (profile.type == AudioType.Music)
                {
                    PlayMusic(profile.clip);
                }
                else
                {
                    PlaySfx(profile.clip);
                }
            }
        }
        void PlayMusic(AudioClip clip)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        void PlaySfx(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public enum AudioType
    {
        Sound,
        Music
    }

    public enum State
    {
        Click,
        Satisfy,
        Drag,
        Drop,
        GameOver,
        Gameplay,
    }
}