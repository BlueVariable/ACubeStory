using System;
using BLUE.EventRaiserSystem;

namespace BLUE.ACubeStory.LoadingScreen
{
    public class EventRaiserArgs_OnLoadingScreenRequested : EventArgs
    {
        public bool Activation { get; }
        public Action<bool> ResultedCallback { get; }

        public EventRaiserArgs_OnLoadingScreenRequested(bool activation, Action<bool> resultedCallback = null)
        {
            Activation = activation;
            ResultedCallback = resultedCallback;
        }
    }
    
    public class EventRaiser_OnLoadingScreenRequested 
        : EventRaiserBase<EventRaiserArgs_OnLoadingScreenRequested>
    {
        
    }
}