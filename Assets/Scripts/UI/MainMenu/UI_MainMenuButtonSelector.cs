using System;
using BLUE.ACubeStory.Cube;
using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class UI_MainMenuButtonSelector : UIBase
    {
        [SerializeField] private UI_MainMenuButton[] _mainMenuButtons = new UI_MainMenuButton[0];

        [SerializeField] private UI_MainMenuButton _initialMainMenuButton = null;

        private UI_MainMenuButton _curSelectedMainMenuButton;

        private float _lastZPoint;
        
        protected override void ActivatedCustomActions()
        {
            _curSelectedMainMenuButton = _initialMainMenuButton;
            
            _curSelectedMainMenuButton.ButtonSelected();

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

            _lastZPoint = Cube.Cube.Instance.gameObject.transform.position.z;
        }

        private void OnStateExited(ECubeState state)
        {
            if (state != ECubeState.Roll)
            {
                return;
            }

            UI_MainMenuButton potentialButton = GetCorrectMainMenuButton();

            if (potentialButton == _curSelectedMainMenuButton)
            {
                return;
            }
            
            _curSelectedMainMenuButton.ButtonDeselected();

            _curSelectedMainMenuButton = potentialButton;
            
            potentialButton.ButtonSelected();
        }

        private UI_MainMenuButton GetCorrectMainMenuButton()
        {
            int rollDirection = (int) Mathf.Sign(Cube.Cube.Instance.gameObject.transform.position.z - _lastZPoint);

            int buttonIndex = Mathf.Clamp(
                Array.IndexOf(_mainMenuButtons, _curSelectedMainMenuButton) + rollDirection, 
                0, 
                _mainMenuButtons.Length - 1);

            return _mainMenuButtons[buttonIndex];
        }
    }
}