using UnityEngine;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Audio.EventRaisers;

namespace BLUE.ACubeStory.Audio
{
    public class Action_AudioControllerSetCanPlayAudio : ActionBase
    {
        [SerializeField] private bool _canPlayAudio = false;

        private EventRaiser_UpdateCanPlayAudioRequest _eventRaiser = 
            new EventRaiser_UpdateCanPlayAudioRequest();
        
        public override void Execute()
        {
            _eventRaiser.Raise(new EventRaiserArgs_UpdateCanPlayAudioRequest(_canPlayAudio));
        }
    }
}