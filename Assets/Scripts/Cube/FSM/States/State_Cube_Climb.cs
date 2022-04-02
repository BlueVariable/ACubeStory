using BLUE.ACubeStory.Common;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Climb : StateBase<ECubeState, FSMTransitionMessage_Cube_Roll>
    {
        [Header("Object Settings")]
        [SerializeField] private FSM_Cube _cubeFSM = null;
        [SerializeField] private Transform _objectToClimb = null;
        
        [Header("Raycast Settings")]
        [SerializeField] private float _rollRayDistance = 1f;
        [SerializeField] private float _climbRayDistance = 1f;
        [SerializeField] private LayerMask _layerMask = default;

        [Header("Roll Settings")]
        [SerializeField] private float _rollSpeed = 2f;
        [SerializeField] private AnimationCurve _rollCurve = null;
        [SerializeField] private RollBehaviourProviderBase _rollBehaviourProvider = null;
        
        public override ECubeState GetStateType()
        {
            return ECubeState.Climb;
        }

        protected override void EnterStateCustomActions(
            FSMTransitionMessage_Cube_Roll transitionMessage)
        {
            _rollBehaviourProvider.GetRollBehaviour().TryRollTo(
                transitionMessage.Direction,
                180f,
                _rollSpeed,
                new Vector3(0, 1, 0),
                _rollCurve,
                onCompleted);

            void onCompleted()
            {
                _cubeFSM.TryChangeState(ECubeState.Idle);
            }
        }

        protected override bool CanEnterStateCustomActions(FSMTransitionMessage_Cube_Roll transitionMessage)
        {
            return !CanRoll(transitionMessage.Direction) && CanClimb(transitionMessage.Direction);
        }
        
        public bool CanRoll(Vector3 direction)
        {
            return !Physics.Raycast(_objectToClimb.position, direction,
                out RaycastHit hit, _rollRayDistance, _layerMask);
        }

        public bool CanClimb(Vector3 direction)
        {
            Vector3 rayFrom = _objectToClimb.position + _climbRayDistance * Vector3.up;
            
            return !Physics.Raycast(rayFrom, direction,
                out RaycastHit hit, _rollRayDistance, _layerMask);
        }
    }
}