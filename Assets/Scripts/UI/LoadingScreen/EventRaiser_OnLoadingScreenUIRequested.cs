using System;
using BLUE.EventRaiserSystem;

namespace BLUE.ACubeStory.UI
{
    public class EventRaiserArgs_OnLoadingScreenUIRequested : EventArgs
    {
        public bool Activation { get; }
        public Action<bool> ResultedCallback { get; }

        public EventRaiserArgs_OnLoadingScreenUIRequested(bool activation, Action<bool> resultedCallback = null)
        {
            Activation = activation;
            ResultedCallback = resultedCallback;
        }
    }
    
    public class EventRaiser_OnLoadingScreenUIRequested 
        : EventRaiserBase<EventRaiserArgs_OnLoadingScreenUIRequested>
    {
        
    }
}