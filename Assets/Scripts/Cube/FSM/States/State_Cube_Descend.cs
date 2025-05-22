using BLUE.ActionSystem;
using BLUE.ACubeStory.Common;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Descend : StateBase<ECubeState, FSMTransitionMessage_Cube_Roll>
    {
        [Header("Object Settings")]
        [SerializeField] private FSM_Cube _cubeFSM = null;
        [SerializeField] private Transform _objectToDescend = null;
        
        [Header("Raycast Settings")]
        [SerializeField] private float _rollRayDistance = 1f;
        [SerializeField] private float _descendRayDistance = 2f;
        [SerializeField] private LayerMask _rollLayerMask = default;
        [SerializeField] private LayerMask _descendLayerMask = default;
        
        [Header("Roll Settings")]
        [SerializeField] private float _rollSpeed = 2f;
        [SerializeField] private AnimationCurve _rollCurve = null;
        [SerializeField] private RollBehaviourProviderBase _rollBehaviourProvider = null;

        [Header("Action Executors")] 
        [SerializeField] private ActionExecutor _descendStartedActionExecutor = null;
        [SerializeField] private ActionExecutor _descendCompletedActionExecutor = null;
        
        public FSMTransitionMessage_Cube_Roll LastCandidateTransitionMessage { get; private set; }

        public override ECubeState GetStateType()
        {
            return ECubeState.Descend;
        }

        protected override void EnterStateCustomActions(
            FSMTransitionMessage_Cube_Roll transitionMessage)
        {
            bool canRoll = _rollBehaviourProvider.GetRollBehaviour().TryRollTo(
                transitionMessage.Direction, 
                180,
                _rollSpeed,
                rollCurve: _rollCurve,
                onRolledCallback: onCompleted);

            if (canRoll)
            {
                _descendStartedActionExecutor.Execute();
            }
            
            void onCompleted()
            {
                _descendCompletedActionExecutor.Execute();

                _cubeFSM.TryChangeState(ECubeState.Idle);
            }
        }

        protected override bool CanEnterStateCustomActions(FSMTransitionMessage_Cube_Roll transitionMessage)
        {
            LastCandidateTransitionMessage = transitionMessage;

            return CanRoll(transitionMessage.Direction) && CanDescend(transitionMessage.Direction);
        }

        public bool CanRoll(Vector3 direction)
        {
            return !Physics.Raycast(_objectToDescend.position, direction,
                out RaycastHit hit, _rollRayDistance, _rollLayerMask);
        }
        
        public bool CanDescend(Vector3 direction)
        {
            Vector3 rayFrom = _objectToDescend.position + _rollRayDistance * direction;

            if (!IsBelowVoid(rayFrom))
            {
                return false;
            }
            
            return Physics.Raycast(rayFrom, Vector3.down,
                out RaycastHit hit, _descendRayDistance, _descendLayerMask);
        }

        public bool IsBelowVoid(Vector3 position)
        {
            return !Physics.Raycast(position, Vector3.down,
                out RaycastHit hit, _rollRayDistance, _rollLayerMask);
        }
    }
}