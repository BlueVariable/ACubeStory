using System;
using System.Linq;
using BLUE.SoundSystem;
using DG.Tweening;
using UnityEngine;

namespace BLUE.ACubeStory.Music
{
    public class SoundPlayer_Music : SoundPlayerBase<SoundPlayData_Music>
    {
        #region Singleton

        private static SoundPlayer_Music _instance;
        public static SoundPlayer_Music Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SoundPlayer_Music>();
                }

                return _instance;
            }
        }

        #endregion
        
        private MusicProviderBase[] _musicProviders;
        private MusicProviderBase[] MusicProviders
        {
            get
            {
                if (_musicProviders == null)
                {
                    _musicProviders = GetComponentsInChildren<MusicProviderBase>();
                }

                return _musicProviders;
            }
        }
        
        public override bool IsPlaying { get; protected set; }
        public bool IsBlending { get; protected set; }
        
        public MusicProviderBase CurrentMusic { get; private set; }
        public MusicProviderBase NextMusic { get; private set; }

        private Tweener _fadeInTween;
        private Tweener _fadeOutTween;
        
        public override void Play(
            SoundPlayData_Music playData,
            Action onPlayed = null)
        {
            MusicProviderBase nextMusicProvider = MusicProviders.FirstOrDefault(i => i.GetMusicType() == playData.MusicType);
         
            if (nextMusicProvider == null)
            {
                return;
            }

            NextMusic = nextMusicProvider;
            
            PlayAudioSource(
                nextMusicProvider.GetAudioSource(), 
                playData);
            
            onPlayed?.Invoke();
        }
        
        public override void Stop()
        {
            IsPlaying = false;
            IsBlending = false;

            _fadeInTween?.Kill();
            _fadeOutTween?.Kill();
            
            if (CurrentMusic)
            {
                CurrentMusic.GetAudioSource().Stop();
                CurrentMusic = null;
            }
            
            if (NextMusic)
            {
                NextMusic.GetAudioSource().Stop();
                NextMusic = null;
            }
        }

        public void FadeOut(float fadeDuration, Action onFaded = null)
        {
            FadeAudio(CurrentMusic.GetAudioSource(), fadeDuration, true, onFaded = null);
        }

        private void PlayAudioSource(AudioSource audioSource, SoundPlayData_Music playData)
        {
            if (CurrentMusic)
            {
                FadeAudio(CurrentMusic.GetAudioSource(), playData.BlendDuration, true);
            }

            FadeAudio(audioSource, playData.BlendDuration, false, onFaded);

            void onFaded()
            {
                CurrentMusic = NextMusic;
                NextMusic = null;
            }
        }

        private void FadeAudio(AudioSource audioSource, float fadeDuration, bool isOut = true, Action onFaded = null)
        {
            IsPlaying = true;
            IsBlending = true;

            float currentVolume = audioSource.volume;
            
            float endValue = isOut ? 0 : currentVolume;

            if (isOut)
            {
                _fadeOutTween?.Kill();
                _fadeOutTween = audioSource
                    .DOFade(endValue, fadeDuration)
                    .OnComplete(() =>
                    {
                        audioSource.Stop();
                        audioSource.volume = currentVolume;
                        IsBlending = false;
                        onFaded?.Invoke();
                    });
            }
            else
            {
                audioSource.Play();
                audioSource.volume = 0;
                
                _fadeInTween?.Kill();
                _fadeInTween = audioSource
                    .DOFade(endValue, fadeDuration)
                    .OnComplete(() =>
                    {
                        IsBlending = false;
                        onFaded?.Invoke();
                    });
            }
        }

        private void OnDestroy()
        {
            _fadeInTween?.Kill();
            _fadeOutTween?.Kill();
        }
    }
}