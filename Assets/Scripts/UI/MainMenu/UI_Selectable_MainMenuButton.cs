using BLUE.TriggerSystem;
using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class UI_Selectable_MainMenuButton : UI_SelectableBase
    {
        [SerializeField] private EMainMenuButton _mainMenuButtonType = EMainMenuButton.None;
        public EMainMenuButton MainMenuButtonType => _mainMenuButtonType;
        
        [SerializeField] private TriggerBase _buttonTrigger = null;

        private EventRaiser_OnMainMenuButtonTriggered _mainMenuButtonTriggeredRaiser =
            new EventRaiser_OnMainMenuButtonTriggered();
        
        protected override void SelectedCustomActions()
        {
            RegisterToButtonTrigger();
            
            base.SelectedCustomActions();
        }

        protected override void UnselectedCustomActions()
        {
            UnregisterFromButtonTrigger();
            
            base.UnselectedCustomActions();
        }

        private void RegisterToButtonTrigger()
        {
            _buttonTrigger.OnTriggered += OnTriggered;
        }

        private void UnregisterFromButtonTrigger()
        {
            _buttonTrigger.OnTriggered -= OnTriggered;
        }

        private void OnTriggered(TriggerBase trigger)
        {
            _mainMenuButtonTriggeredRaiser.Raise(new EventArgs_OnMainMenuButtonTriggered(MainMenuButtonType));
        }
    }
}