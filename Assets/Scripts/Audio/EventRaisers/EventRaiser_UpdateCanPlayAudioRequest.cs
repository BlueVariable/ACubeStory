using System;
using BLUE.EventRaiserSystem;

namespace BLUE.ACubeStory.Audio.EventRaisers
{
    public class EventRaiserArgs_UpdateCanPlayAudioRequest : EventArgs
    {
        public bool CanPlayAudio { get; }

        public EventRaiserArgs_UpdateCanPlayAudioRequest(bool canPlayAudio)
        {
            CanPlayAudio = canPlayAudio;
        }
    }
    
    public class EventRaiser_UpdateCanPlayAudioRequest : EventRaiserBase<EventRaiserArgs_UpdateCanPlayAudioRequest>
    {
        
    }
}