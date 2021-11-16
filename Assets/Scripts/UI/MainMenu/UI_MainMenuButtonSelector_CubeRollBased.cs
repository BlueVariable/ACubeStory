using System;
using BLUE.ACubeStory.Cube;
using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class UI_MainMenuButtonSelector_CubeRollBased : UI_ButtonSelectorBase<UI_MainMenuButton>
    {
        [SerializeField] private UI_MainMenuButton[] _mainMenuButtons = new UI_MainMenuButton[0]; 
        
        [SerializeField] private UI_MainMenuButton _initialMainMenuButton = null;
        
        private float _lastZPoint;
        
        protected override void ActivatedCustomActions()
        {
            TrySelectButton(_initialMainMenuButton);
            
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

            UI_MainMenuButton potentialButton = GetPotentialMainMenuButton();

            TrySelectButton(potentialButton);
        }

        private UI_MainMenuButton GetPotentialMainMenuButton()
        {
            float curZPoint = Cube.Cube.Instance.GetPosition().z;

            TryGetSingleSelectedButton(out UI_MainMenuButton curButton);
            
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