using UnityEngine;
using BLUE.ActionSystem;

namespace BLUE.ACubeStory.Audio
{
    public class Action_RequestAudioToggle : ActionBase
    {
        [SerializeField] private bool _canPlayAudio = false;

        private EventRaiser_OnAudioToggleRequested _eventRaiser = 
            new EventRaiser_OnAudioToggleRequested();
        
        public override void Execute()
        {
            _eventRaiser.Raise(new EventRaiserArgs_OnAudioToggleRequested(_canPlayAudio));
        }
    }
}