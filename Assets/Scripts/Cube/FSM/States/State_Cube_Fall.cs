using BLUE.Common;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Fall : StateBase<ECubeState, FSMTransitionMessage_Cube>
    {
        [Header("Object Settings")]
        [SerializeField] private FSM_Cube _cubeFSM = null;
        [SerializeField] private Transform _objectToFall = null;
        [SerializeField] private Vector3 _objectPivotOffset = new Vector3(0, 0.5f, 0);
        
        [Header("Raycast Settings")]
        [SerializeField] private float _maxRayDistance = 100f;
        [SerializeField] private LayerMask _layerMask = default;
        [SerializeField] private float _minValidFallDistance = 0.5f;
        
        [Header("Move Settings")]
        [SerializeField] private MoveBehaviour_Tween _moveBehaviour = null;
        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _moveSettings = null;
        
        public override ECubeState GetStateType()
        {
            return ECubeState.Fall;
        }

        protected override void EnterStateCustomActions(
            FSMTransitionMessage_Cube transitionMessage)
        {
            if (Physics.Raycast(_objectToFall.position, Vector3.down, 
                out RaycastHit hit, _maxRayDistance, _layerMask))
            {
                if (hit.distance < _minValidFallDistance)
                {
                    _cubeFSM.TryChangeState(ECubeState.Idle);

                    return;
                }
                
                void onFallCompleted()
                {
                    _cubeFSM.TryChangeState(ECubeState.Idle);
                }
                    
                _moveBehaviour.StartMovementTowards(_moveSettings, hit.point + _objectPivotOffset, 
                    onFallCompleted);
            }
            else
            {
                _cubeFSM.TryChangeState(ECubeState.Idle);
            }
        }
    }
}