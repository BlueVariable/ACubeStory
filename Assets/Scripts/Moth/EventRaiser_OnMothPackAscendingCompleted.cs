using System;
using BLUE.EventRaiserSystem;

namespace BLUE.ACubeStory.Moth
{
    public class EventRaiserArgs_OnMothPackAscendingCompleted : EventArgs
    {
        public MothPackAscendingController MothPackAscendingController { get; }

        public EventRaiserArgs_OnMothPackAscendingCompleted(MothPackAscendingController mothPackAscendingController)
        {
            MothPackAscendingController = mothPackAscendingController;
        }
    }
    
    public class EventRaiser_OnMothPackAscendingCompleted : EventRaiserBase<EventRaiserArgs_OnMothPackAscendingCompleted>
    {
        
    }
}