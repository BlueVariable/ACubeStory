using System;
using BLUE.EventRaiserSystem;

namespace BLUE.ACubeStory.UI
{
    public class EventRaiserArgs_OnCutsceneUIRequested : EventArgs
    {
        public bool Activation { get; }
        public Action<bool> ResultedCallback { get; }

        public EventRaiserArgs_OnCutsceneUIRequested(bool activation, Action<bool> resultedCallback = null)
        {
            Activation = activation;
            ResultedCallback = resultedCallback;
        }
    }
    
    public class EventRaiser_OnCutsceneUIRequested : EventRaiserBase<EventRaiserArgs_OnCutsceneUIRequested>
    {
        
    }
}