using System;
using System.Collections;
using System.Linq;
using BLUE.ACubeStory.Scene;
using UnityEngine;

namespace BLUE.ACubeStory.Audio
{
    public class AudioController_SceneBased : AudioControllerBase
    {
        [Serializable]
        private class SceneAudioMapData
        {
            [SerializeField] private EScene _scene = EScene.Base;
            public EScene Scene => _scene;

            [SerializeField] private AudioSource _audioSource = null;
            public AudioSource AudioSource => _audioSource;
        }
        
        [SerializeField] private SceneAudioMapData[] _sceneAudioMappings = null;

        [SerializeField] private float _volumeUpdateDuration = 0.5f;

        [SerializeField] private float _delayBetweenAudios = 1f;

        private SceneAudioMapData _nowPlaying = null;

        private IEnumerator _audioSwitchRoutine;
        
        public override EAudioController GetAudioControllerType()
        {
            return EAudioController.SceneBased;
        }

        private void Awake()
        {
            RegisterToSceneManager();
        }

        private void OnDestroy()
        {
            UnregisterFromSceneManager();
        }

        private void RegisterToSceneManager()
        {
            SceneManager.Instance.OnPostSceneLoad += OnSceneLoaded;
        }

        private void UnregisterFromSceneManager()
        {
            if (SceneManager.Instance != null)
            {
                SceneManager.Instance.OnPostSceneLoad -= OnSceneLoaded;
            }
        }

        private void OnSceneLoaded(EScene eScene)
        {
            if (!CanPlayAudio)
            {
                return;
            }
            
            SceneAudioMapData candidateMapping = _sceneAudioMappings.SingleOrDefault(i => i.Scene == eScene);

            if (candidateMapping == null)
            {
                return;
            }

            if (_nowPlaying != null && _nowPlaying.AudioSource == candidateMapping.AudioSource)
            {
                return;
            }

            StartAudioSwitchRoutine(candidateMapping);
        }

        private void StartAudioSwitchRoutine(SceneAudioMapData nextAudioMapping)
        {
            StopAudioSwitchRoutine();

            _audioSwitchRoutine = AudioSwitchProgress(nextAudioMapping);

            StartCoroutine(_audioSwitchRoutine);
        }

        private void StopAudioSwitchRoutine()
        {
            if (_audioSwitchRoutine != null)
            {
                StopCoroutine(_audioSwitchRoutine);
            }
        }

        private IEnumerator AudioSwitchProgress(SceneAudioMapData nextAudioMapping)
        {
            if (_nowPlaying != null)
            {
                yield return AudioVolumeUpdateProgress(_nowPlaying.AudioSource, 0);
                
                _nowPlaying.AudioSource.Stop();
                
                yield return new WaitForSeconds(_delayBetweenAudios);
            }

            _nowPlaying = nextAudioMapping;

            _nowPlaying.AudioSource.volume = 0;
            
            _nowPlaying.AudioSource.Play();
            
            yield return AudioVolumeUpdateProgress(_nowPlaying.AudioSource, 1);
        }

        private IEnumerator AudioVolumeUpdateProgress(AudioSource audioSource, float to)
        {
            float timePassed = 0;

            float startingVal = audioSource.volume;

            while (timePassed < _volumeUpdateDuration)
            {
                timePassed += Time.deltaTime;

                audioSource.volume = Mathf.Lerp(startingVal, to, timePassed / _volumeUpdateDuration);

                yield return null;
            }

            audioSource.volume = to;
        }
    }
}