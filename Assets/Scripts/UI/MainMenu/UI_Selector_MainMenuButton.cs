using System;
using BLUE.ACubeStory.Cube;
using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class UI_Selector_MainMenuButton : UI_SelectorBase<UI_Selectable_MainMenuButton>
    {
        [SerializeField] private UI_Selectable_MainMenuButton[] _mainMenuButtons = new UI_Selectable_MainMenuButton[0]; 
        
        [SerializeField] private UI_Selectable_MainMenuButton _initialSelectableMainMenuButton = null;
        
        private float _lastZPoint;
        
        protected override void ActivatedCustomActions()
        {
            TrySelectSelectable(_initialSelectableMainMenuButton);
            
            RegisterToCubeFSM();

            base.ActivatedCustomActions();
        }

        protected override void DeactivatedCustomActions()
        {
            UnregisterFromCubeFSM();

            base.DeactivatedCustomActions();
        }

        private void RegisterToCubeFSM()
        {
            Cube.Cube.Instance.CubeFSM.OnStateEntered += OnStateEntered;
            Cube.Cube.Instance.CubeFSM.OnStateExited += OnStateExited;
        }

        private void UnregisterFromCubeFSM()
        {
            Cube.Cube.Instance.CubeFSM.OnStateEntered -= OnStateEntered;
            Cube.Cube.Instance.CubeFSM.OnStateExited -= OnStateExited;  
        }

        private void OnStateEntered(ECubeState state)
        {
            if (state != ECubeState.Roll)
            {
                return;
            }

            _lastZPoint = Cube.Cube.Instance.GetPosition().z;
        }

        private void OnStateExited(ECubeState state)
        {
            if (state != ECubeState.Roll)
            {
                return;
            }

            UI_Selectable_MainMenuButton potentialButton = GetPotentialMainMenuButton();

            TrySelectSelectable(potentialButton);
        }

        private UI_Selectable_MainMenuButton GetPotentialMainMenuButton()
        {
            float curZPoint = Cube.Cube.Instance.GetPosition().z;

            TryGetSingleSelectable(out UI_Selectable_MainMenuButton curButton);
            
            if (curZPoint == _lastZPoint)
            {
                return curButton;
            }
                
            int rollDirection = (int) Mathf.Sign(curZPoint - _lastZPoint);

            int buttonIndex = Mathf.Clamp(
                Array.IndexOf(_mainMenuButtons, curButton) + rollDirection, 
                0, 
                _mainMenuButtons.Length - 1);

            return _mainMenuButtons[buttonIndex];
        }
    }
}