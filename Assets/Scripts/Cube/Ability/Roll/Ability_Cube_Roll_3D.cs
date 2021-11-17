using System;
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

            foreach (ECubeState state in _states)
            {
                if(_cubeFSM.TryChangeState(state, new FSMTransitionMessage_Cube_Roll(direction)))
                {
                    return;
                }
            }
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