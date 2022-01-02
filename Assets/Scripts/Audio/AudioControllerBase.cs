using System;
using BLUE.ACubeStory.Audio.EventRaisers;
using UnityEngine;

namespace BLUE.ACubeStory.Audio
{
    public abstract class AudioControllerBase : MonoBehaviour
    {
        public bool CanPlayAudio { get; private set; } = true;
        
        public abstract EAudioController GetAudioControllerType();

        private void Awake()
        {
            RegisterToEventRaiser();
            
            AwakeCustomActions();
        }

        private void OnDestroy()
        {
            UnregisterFromEventRaiser();

            OnDestroyCustomActions();
        }

        private void RegisterToEventRaiser()
        {
            EventRaiser_UpdateCanPlayAudioRequest.OnRaisedStatic += OnUpdateRequested;
        }

        private void UnregisterFromEventRaiser()
        {
            EventRaiser_UpdateCanPlayAudioRequest.OnRaisedStatic -= OnUpdateRequested;
        }

        private void OnUpdateRequested(object sender, EventRaiserArgs_UpdateCanPlayAudioRequest e)
        {
            CanPlayAudio = e.CanPlayAudio;

            OnCanPlayAudioSet(CanPlayAudio);
        }

        protected virtual void AwakeCustomActions() { }
        
        protected virtual void OnDestroyCustomActions() { }
        
        protected virtual void OnCanPlayAudioSet(bool canPlay) { }
    }
}