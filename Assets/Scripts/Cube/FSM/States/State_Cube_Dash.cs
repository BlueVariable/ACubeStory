using System;
using BLUE.ACubeStory.Common;
using UnityEngine;
using BLUE.Common;
using BLUE.FSMSystem;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Dash : StateBase<ECubeState, FSMTransitionMessage_Cube_Dash>
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;
        
        [SerializeField] private MoveBehaviour_Tween _dashMoveBehaviour = null;

        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _settings = null;

        [SerializeField] private Vector3 _raycastOffset = Vector3.zero;

        [SerializeField] private LayerMask _layerMask = default;
        
        [SerializeField] private int _dashDistance = 2;
        
        [SerializeField] private RollBehaviourProviderBase _rollBehaviourProvider = null;

        public override ECubeState GetStateType()
        {
            return ECubeState.Dash;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_Cube_Dash transitionMessage)
        {
            RollBehaviourBase rollBehaviour = _rollBehaviourProvider.GetRollBehaviour();

            if (rollBehaviour.IsRollPaused)
            {
                Dash(
                    transitionMessage.DashDirection, 
                    _dashDistance,
                    -transitionMessage.DashDirection,
                    onDashCompleted);
            }
            else
            {
                Dash(
                    transitionMessage.DashDirection,
                    _dashDistance,
                    Vector3.zero, 
                    onRollCompleted);
            } 
            
            void onDashCompleted()
            {
                rollBehaviour.TryRollTo(
                    transitionMessage.DashDirection, 
                    90 - rollBehaviour.CurRollAngle,
                    onRolledCallback: onRollCompleted);
            }
            
            void onRollCompleted()
            {
                _cubeFSM.TryChangeState(ECubeState.Fall);
            }
            
            base.EnterStateCustomActions(transitionMessage);
        }

        private void Dash(
            Vector3 dashDirection, 
            int dashDistance, 
            Vector3 destinationOffset,
            Action onDashCompleted)
        {
            _dashMoveBehaviour.StartMovementTowards(
                _settings, 
                TryGetDestination(dashDirection, dashDistance) + destinationOffset,
                onDashCompleted);
        }

        private Vector3 TryGetDestination(Vector3 dashDirection, int completeDashDistance)
        {
            for (int distance = completeDashDistance; distance > 0; distance--)
            {
                bool result = Physics.Raycast(
                    transform.position - _raycastOffset, 
                    dashDirection, 
                    out RaycastHit hit,
                    distance,
                    _layerMask);

                if (!result)
                {
                    return transform.position + dashDirection * distance;
                }
            }

            return transform.position;
        }
    }
}