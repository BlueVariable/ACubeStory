using BLUE.ACubeStory.Ability;
using BLUE.InputSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Ability_Cube_Dash : AbilityBase
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;

        [SerializeField] private KeyCode _dashTriggerKey = KeyCode.LeftShift;
        
        public bool IsPreDashKeyActive { get; private set; }
        
        public override void Attached()
        {
            RegisterToKeyInputEvents();
        }

        public override void Detached()
        {
            UnregisterFromKeyInputEvents();
            
            UnregisterFromMovementInputEvents();
        }

        #region Input Related

        private void RegisterToKeyInputEvents()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyDown, OnKeyDown);
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }

        private void UnregisterFromKeyInputEvents()
        {
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyDown, OnKeyDown);
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }

        private void OnKeyDown(InputData_Key inputData)
        {
            if (inputData.KeyCode != _dashTriggerKey)
            {
                return;
            }

            IsPreDashKeyActive = true;
            
            RegisterToMovementInputEvents();
        }

        private void OnKeyUp(InputData_Key inputData)
        {
            if (inputData.KeyCode != _dashTriggerKey)
            {
                return;
            }

            IsPreDashKeyActive = false;
            
            UnregisterFromMovementInputEvents();
        }

        private void RegisterToMovementInputEvents()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Axis>(EInputEvent.HorizontalAxis,
                OnHorizontalAxisInputPerformed);
            InputManager.Instance.TryRegisterToInputListener<InputData_Axis>(EInputEvent.VerticalAxis,
                OnVerticalAxisInputPerformed);
        }

        private void UnregisterFromMovementInputEvents()
        {
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Axis>(EInputEvent.HorizontalAxis,
                OnHorizontalAxisInputPerformed);
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Axis>(EInputEvent.VerticalAxis,
                OnVerticalAxisInputPerformed);            
        }

        private void OnHorizontalAxisInputPerformed(InputData_Axis inputData)
        {
            if (inputData.InputState == EAxisInputState.Finished)
            {
                return;
            }
            
            TryPerformDash(new Vector2(Mathf.Sign(inputData.Amount), 0));
        }

        private void OnVerticalAxisInputPerformed(InputData_Axis inputData)
        {
            if (inputData.InputState == EAxisInputState.Finished)
            {
                return;
            }
            
            TryPerformDash(new Vector2(0, Mathf.Sign(inputData.Amount)));
        }

        #endregion
        
        private bool TryPerformDash(Vector2 dashDirection)
        {
            return _cubeFSM.TryChangeState(ECubeState.Dash, 
                new FSMTransitionMessage_Cube_Dash(new Vector3(-dashDirection.y, 0, dashDirection.x)));
        }
    }
}