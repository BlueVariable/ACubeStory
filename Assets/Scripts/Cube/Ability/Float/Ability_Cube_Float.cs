using BLUE.ACubeStory.Ability;
using BLUE.InputSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Ability_Cube_Float : AbilityBase
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

        private void OnDestroy()
        {
            UnregisterFromInputManager();
        }

        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Axis>
                (EInputEvent.HorizontalAxis, OnHorizontalInputPerformed);
            
            InputManager.Instance.TryRegisterToInputListener<InputData_Axis>
                (EInputEvent.VerticalAxis, OnVerticalInputPerformed);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Axis>
                (EInputEvent.HorizontalAxis, OnHorizontalInputPerformed);
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Axis>
                (EInputEvent.VerticalAxis, OnVerticalInputPerformed);
        }

        private void OnHorizontalInputPerformed(InputData_Axis axisInputData)
        {
            if (axisInputData.InputState == EAxisInputState.Finished)
            {
                return;
            }
            
            Vector3 direction = 
                axisInputData.Amount < 0
                ? Vector3.back
                : Vector3.forward;

            _cubeFSM.TryChangeState(ECubeState.Float, new FSMTransitionMessage_Cube_Float(direction));
        }
        
        private void OnVerticalInputPerformed(InputData_Axis verticalAxisInputData)
        {
            if (verticalAxisInputData.InputState == EAxisInputState.Finished)
            {
                return;
            }

            Vector3 direction = 
                verticalAxisInputData.Amount < 0
                    ? Vector3.right
                    : Vector3.left;

            _cubeFSM.TryChangeState(ECubeState.Float, new FSMTransitionMessage_Cube_Float(direction));
        }
    }
}