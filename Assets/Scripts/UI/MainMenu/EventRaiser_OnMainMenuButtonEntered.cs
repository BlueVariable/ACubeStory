using System;
using BLUE.EventRaiserSystem;

namespace BLUE.ACubeStory.UI
{
    public class EventArgs_OnMainMenuButtonEntered : EventArgs
    {
        public EMainMenuButton Button { get; }
        
        public EventArgs_OnMainMenuButtonEntered(EMainMenuButton button)
        {
            Button = button;
        }
    }

    public class EventRaiser_OnMainMenuButtonEntered : EventRaiserBase<EventArgs_OnMainMenuButtonEntered>
    {
    }
}