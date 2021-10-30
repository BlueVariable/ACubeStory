using BLUE.ACubeStory.Ability;
using BLUE.ACubeStory.Common;
using BLUE.InputSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Ability_Cube_Roll_2D : AbilityBase
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;

        public override void Attached()
        {
            RegisterToInputManager();
        }

        public override void Detached()
        {
            UnregisterFromInputManager();
        }
        
        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_HorizontalAxis>
                (EInputEvent.HorizontalAxis, OnHorizontalInputPerformed);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_HorizontalAxis>
                (EInputEvent.HorizontalAxis, OnHorizontalInputPerformed);
        }

        private void OnHorizontalInputPerformed(InputData_HorizontalAxis horizontalAxisInputData)
        {
            if (horizontalAxisInputData.InputState == EHorizontalAxisInputState.Finished)
            {
                return;
            }

            Vector3 direction = 
                horizontalAxisInputData.Amount < 0
                    ? Vector3.back
                    : Vector3.forward;

            _cubeFSM.TryChangeState(ECubeState.Roll, new FSMTransitionMessage_Cube_Roll(direction));
        }
    }
}