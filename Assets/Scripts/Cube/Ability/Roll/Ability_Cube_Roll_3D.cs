using BLUE.ACubeStory.Ability;
using BLUE.ACubeStory.Common;
using BLUE.InputSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Ability_Cube_Roll_3D : AbilityBase
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
            RollBehaviourBase.ERollDirection direction = 
                horizontalAxisInputData.Amount < 0
                ? RollBehaviourBase.ERollDirection.Forward
                : RollBehaviourBase.ERollDirection.Backward;

            _cubeFSM.TryChangeState(ECubeState.Roll, new FSMTransitionMessage_Cube_Roll(direction));
        }
        
        private void OnVerticalInputPerformed(InputData_VerticalAxis verticalAxisInputData)
        {
            RollBehaviourBase.ERollDirection direction = 
                verticalAxisInputData.Amount < 0
                    ? RollBehaviourBase.ERollDirection.Left
                    : RollBehaviourBase.ERollDirection.Right;

            _cubeFSM.TryChangeState(ECubeState.Roll, new FSMTransitionMessage_Cube_Roll(direction));
        }
    }
}