using System;
using BLUE.Common;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Dash : StateBase<ECubeState, FSMTransitionMessage_Cube_Dash>
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;
        
        [SerializeField] private MoveBehaviour_Tween _dashMoveBehaviour = null;

        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _settings = null;

        [SerializeField] private ParticleSystem _dashParticle = null;
        
        [SerializeField] private Vector3 _raycastOffset = Vector3.zero;

        [SerializeField] private LayerMask _layerMask = default;
        
        [SerializeField] private int _dashDistance = 2;

        public Action<Vector3> OnEnteredDashState { get; set; }

        public override ECubeState GetStateType()
        {
            return ECubeState.Dash;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_Cube_Dash transitionMessage)
        {
            Dash(
                transitionMessage.DashDirection,
                _dashDistance,
                Vector3.zero, 
                onDashCompleted);
            
            void onDashCompleted()
            {
                _cubeFSM.TryChangeState(ECubeState.Fall);
            }
            
            OnEnteredDashState?.Invoke(transitionMessage.DashDirection);
            
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