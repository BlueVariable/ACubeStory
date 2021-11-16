using System;
using BLUE.EventRaiserSystem;

namespace BLUE.ACubeStory.UI
{
    public class EventArgs_OnMainMenuButtonTriggered : EventArgs
    {
        public EMainMenuButton Button { get; }
        
        public EventArgs_OnMainMenuButtonTriggered(EMainMenuButton button)
        {
            Button = button;
        }
    }

    public class EventRaiser_OnMainMenuButtonTriggered : EventRaiserBase<EventArgs_OnMainMenuButtonTriggered>
    {
    }
}