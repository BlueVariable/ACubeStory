using System;
using BLUE.EventRaiserSystem;

namespace BLUE.ACubeStory.Audio
{
    public class EventRaiserArgs_OnAudioToggleRequested : EventArgs
    {
        public bool Toggle { get; }

        public EventRaiserArgs_OnAudioToggleRequested(bool toggle)
        {
            Toggle = toggle;
        }
    }
    
    public class EventRaiser_OnAudioToggleRequested : EventRaiserBase<EventRaiserArgs_OnAudioToggleRequested>
    {
        
    }
}