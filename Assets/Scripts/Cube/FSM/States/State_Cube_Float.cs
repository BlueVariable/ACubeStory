using BLUE.FSMSystem;
using BLUE.InputSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Float : StateBase<ECubeState, FSMTransitionMessage_Cube_Float>
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;

        [Header("Move Settings")]
        [SerializeField] private Transform _transformToFloat = null;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotateSpeed = 10f;
        
        [Header("Raycast Settings")]
        [SerializeField] private float _floatRayDistance = 0.2f;
        [SerializeField] private LayerMask _layerMask = default;
        
        public override ECubeState GetStateType()
        {
            return ECubeState.Float;
        }
        
        protected override bool CanEnterStateCustomActions(FSMTransitionMessage_Cube_Float transitionMessage)
        {
            return true;
        }
        
        protected override void EnterStateCustomActions(
            FSMTransitionMessage_Cube_Float transitionMessage)
        {
            RegisterToInputManager();
        }

        protected override void ExitStateCustomActions()
        {
            UnregisterFromInputManager();
            
            base.ExitStateCustomActions();
        }

        private void OnDestroy()
        {
            UnregisterFromInputManager();
        }

        private void Move(Vector3 direction)
        {
            if (!CanMove(direction))
            {
                return;
            }

            Vector3 targetPos = _transformToFloat.position + direction;
            Quaternion targetRot = Quaternion.LookRotation(direction, Vector3.up);
            
            _transformToFloat.rotation = Quaternion.Lerp(_transformToFloat.rotation, targetRot, _rotateSpeed * Time.deltaTime);
            _transformToFloat.position = Vector3.Lerp(_transformToFloat.position, targetPos, _moveSpeed * Time.deltaTime);
        }
                
        private bool CanMove(Vector3 direction)
        {
            return !Physics.Raycast(_transformToFloat.position, direction,
                out RaycastHit hit, _floatRayDistance, _layerMask);
        }
        
        #region InputRelated
        
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
                _cubeFSM.TryChangeState(ECubeState.Idle);

                return;
            }
            
            Vector3 direction = 
                axisInputData.Amount < 0
                    ? Vector3.back
                    : Vector3.forward;
            
            Move(direction);
        }
        
        private void OnVerticalInputPerformed(InputData_Axis verticalAxisInputData)
        {
            if (verticalAxisInputData.InputState == EAxisInputState.Finished)
            {
                _cubeFSM.TryChangeState(ECubeState.Idle);

                return;
            }
            
            Vector3 direction = 
                verticalAxisInputData.Amount < 0
                    ? Vector3.right
                    : Vector3.left;
            
            Move(direction);
        }

        #endregion
    }
}