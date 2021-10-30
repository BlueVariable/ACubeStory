using BLUE.ACubeStory.Ability;
using BLUE.ACubeStory.Common;
using BLUE.InputSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Ability_Cube_Roll_3D : AbilityBase
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;

        [SerializeField] private ECubeState[] _states = new ECubeState[0];
        
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
            
            InputManager.Instance.TryRegisterToInputListener<InputData_VerticalAxis>
                (EInputEvent.VerticalAxis, OnVerticalInputPerformed);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_HorizontalAxis>
                (EInputEvent.HorizontalAxis, OnHorizontalInputPerformed);
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_VerticalAxis>
                (EInputEvent.VerticalAxis, OnVerticalInputPerformed);
        }

        private void OnHorizontalInputPerformed(InputData_HorizontalAxis horizontalAxisInputData)
        {
            Vector3 direction = 
                horizontalAxisInputData.Amount < 0
                ? Vector3.back
                : Vector3.forward;

            foreach (ECubeState state in _states)
            {
                if(_cubeFSM.TryChangeState(state, new FSMTransitionMessage_Cube_Roll(direction)))
                {
                    return;
                }
            }
        }
        
        private void OnVerticalInputPerformed(InputData_VerticalAxis verticalAxisInputData)
        {
            Vector3 direction = 
                verticalAxisInputData.Amount < 0
                    ? Vector3.right
                    : Vector3.left;

            foreach (ECubeState state in _states)
            {
                if(_cubeFSM.TryChangeState(state, new FSMTransitionMessage_Cube_Roll(direction)))
                {
                    return;
                }
            }
        }
    }
}