using BLUE.TriggerSystem;
using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class UI_MainMenuButton : UI_SelectorButtonBase
    {
        [SerializeField] private EMainMenuButton _mainMenuButtonType = EMainMenuButton.None;
        public EMainMenuButton MainMenuButtonType => _mainMenuButtonType;
        
        [SerializeField] private TriggerBase _buttonEnterTrigger = null;

        private EventRaiser_OnMainMenuButtonEntered _mainMenuButtonEnteredRaiser =
            new EventRaiser_OnMainMenuButtonEntered();
        
        protected override void ButtonSelectedCustomActions()
        {
            RegisterToButtonEnterTrigger();
            
            base.ButtonSelectedCustomActions();
        }

        protected override void ButtonDeselectedCustomActions()
        {
            UnregisterFromButtonEnterTrigger();
            
            base.ButtonDeselectedCustomActions();
        }

        private void RegisterToButtonEnterTrigger()
        {
            _buttonEnterTrigger.OnTriggered += OnTriggered;
        }

        private void UnregisterFromButtonEnterTrigger()
        {
            _buttonEnterTrigger.OnTriggered -= OnTriggered;
        }

        private void OnTriggered(TriggerBase trigger)
        {
            _mainMenuButtonEnteredRaiser.Raise(new EventArgs_OnMainMenuButtonEntered(MainMenuButtonType));
        }
    }
}