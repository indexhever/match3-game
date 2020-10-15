using Math3Game.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Math3Game
{
    public class SoundInstaller : MonoInstaller, BackgroundMusicController, MatchSoundController, SwapSoundController
    {
        [SerializeField]
        private AudioClip backgroundMusic;
        [SerializeField]
        private AudioClip matchSFX;
        [SerializeField]
        private AudioClip swapSFX;
        [SerializeField]
        private AudioSource musicAudioSource;
        [SerializeField]
        private AudioSource sfxAudioSource;

        public override void InstallBindings()
        {
            Container.Bind<BackgroundMusicController>()
                     .FromInstance(this);

            Container.Bind<MatchSoundController>()
                     .FromInstance(this);

            Container.Bind<SwapSoundController>()
                     .FromInstance(this);
        }

        private new void Start()
        {
            PlayBackgroundMusicSound();
        }

        public void PlayBackgroundMusicSound()
        {
            musicAudioSource.clip = backgroundMusic;
            musicAudioSource.Play();
        }

        public void PlayMatchSound()
        {
            sfxAudioSource.PlayOneShot(matchSFX);
        }

        public void PlaySwapSound()
        {
            sfxAudioSource.PlayOneShot(swapSFX);
        }
    }
}