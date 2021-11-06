using BLUE.Common;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_DashJump : StateBase<ECubeState, FSMTransitionMessage_Cube_Dash>
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;
        
        [SerializeField] private MoveBehaviour_Tween _dashMoveBehaviour = null;

        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _settings = null;

        [SerializeField] private Vector3 _raycastOffset = new Vector3(0, 0.5f, 0);

        [SerializeField] private Vector3 _objectPivotOffset = new Vector3(0.5f, 0.5f, 0.5f);
        
        [SerializeField] private LayerMask _layerMask = default;

        [SerializeField] private int _dashDistance = 2;
        
        
        public override ECubeState GetStateType()
        {
            return ECubeState.Dash_Jump;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_Cube_Dash transitionMessage)
        {
            _dashMoveBehaviour.StartMovementTowards(_settings, TryGetDestination(transitionMessage.DashDirection, _dashDistance), OnDashCompleted);

            base.EnterStateCustomActions(transitionMessage);
        }

        private Vector3 TryGetDestination(Vector3 dashDirection, float completeDashDistance)
        {
            RaycastHit hit;

            bool result = Physics.Raycast(transform.position - _raycastOffset, dashDirection, out hit, completeDashDistance, _layerMask);

            if (!result)
            {
                return transform.position + dashDirection * completeDashDistance;
            }

            Vector3 destination = hit.point;

            destination.y = transform.position.y;

            Vector3 offset = new Vector3(
                _objectPivotOffset.x * dashDirection.x,
                _objectPivotOffset.y * dashDirection.y,
                _objectPivotOffset.z * dashDirection.z);

            destination -= offset;

            return destination;
        }

        private void OnDashCompleted()
        {
            _cubeFSM.TryChangeState(ECubeState.Fall);
        }
    }
}