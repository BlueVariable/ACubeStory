using System;
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

        #region Events

        public Action<Vector3> OnDashStarted { get; set; }
        public Action OnDashCompleted { get; set; }

        #endregion

        public override ECubeState GetStateType()
        {
            return ECubeState.Dash;
        }

        protected override bool CanEnterStateCustomActions(FSMTransitionMessage_Cube_Dash transitionMessage)
        {
            return CheckDashDirectionIsClear(transitionMessage.DashDirection, 1);
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_Cube_Dash transitionMessage)
        {
            OnDashStarted?.Invoke(transitionMessage.DashDirection);
            
            bool performedDash = Dash(
                                    transitionMessage.DashDirection,
                                    _dashDistance,
                                    Vector3.zero, 
                                    onDashCompleted);

            if (!performedDash)
            {
                _cubeFSM.TryChangeState(ECubeState.Fall);
                
                return;
            }
            
            void onDashCompleted()
            {
                OnDashCompleted?.Invoke();
                
                _cubeFSM.TryChangeState(ECubeState.Fall);
            }

            base.EnterStateCustomActions(transitionMessage);
        }

        private bool Dash(
            Vector3 dashDirection, 
            int dashDistance, 
            Vector3 destinationOffset,
            Action onDashCompleted)
        {
            bool targetPosCalculated = TryGetDestination(dashDirection, dashDistance, out Vector3 targetPos);

            targetPos += destinationOffset;

            if (!targetPosCalculated)
            {
                return false;
            }
            
            _dashMoveBehaviour.StartMovementTowards(
                _settings, 
                targetPos,
                onDashCompleted);

            return true;
        }

        private bool TryGetDestination(Vector3 dashDirection, int completeDashDistance, out Vector3 pos)
        {
            for (int distance = completeDashDistance; distance > 0; distance--)
            {
                bool result = CheckDashDirectionIsClear(dashDirection, distance);
                
                if (result)
                {
                    pos = transform.position + dashDirection * distance;
                    return true;
                }
            }

            pos = Vector3.negativeInfinity;
            return false;
        }

        private bool CheckDashDirectionIsClear(Vector3 dashDirection, float distance)
        {
            return !Physics.Raycast(
                transform.position - _raycastOffset, 
                dashDirection, 
                out RaycastHit hit,
                distance,
                _layerMask);
        }
    }
}
