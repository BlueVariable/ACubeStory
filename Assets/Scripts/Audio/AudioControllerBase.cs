using UnityEngine;

namespace BLUE.ACubeStory.Audio
{
    public abstract class AudioControllerBase : MonoBehaviour
    {
        public bool CanPlayAudio { get; private set; } = true;
        
        public abstract EAudioController GetAudioControllerType();

        protected virtual void AwakeCustomActions() { }
        
        protected virtual void OnDestroyCustomActions() { }
        
        protected virtual void OnAudioToggleSet(bool canPlay) { }
        
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
            EventRaiser_OnAudioToggleRequested.OnRaisedStatic += OnToggleRequested;
        }

        private void UnregisterFromEventRaiser()
        {
            EventRaiser_OnAudioToggleRequested.OnRaisedStatic -= OnToggleRequested;
        }

        private void OnToggleRequested(object sender, EventRaiserArgs_OnAudioToggleRequested args)
        {
            if (CanPlayAudio == args.Toggle)
            {
                return;
            }
            
            CanPlayAudio = args.Toggle;

            OnAudioToggleSet(CanPlayAudio);
        }
    }
}