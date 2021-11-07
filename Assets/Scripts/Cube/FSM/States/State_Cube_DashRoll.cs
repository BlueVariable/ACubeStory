using UnityEngine;
using BLUE.Common;
using BLUE.FSMSystem;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_DashRoll : StateBase<ECubeState, FSMTransitionMessage_Cube_Dash>
    {
        [SerializeField] private State_Cube_Roll _rollState = null;

        [SerializeField] private double _rollStateTimeThreshold = 0.25f;

        [SerializeField] private MoveBehaviour_Tween _dashMoveBehaviour = null;

        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _settings = null;

        [SerializeField] private Vector3 _raycastOffset = Vector3.zero;

        [SerializeField] private Vector3 _objectPivotOffset = Vector3.zero;

        [SerializeField] private LayerMask _layerMask = default;
        
        [SerializeField] private int _dashDistance = 2;

        public override ECubeState GetStateType()
        {
            return ECubeState.Dash_Roll;
        }

        protected override bool CanEnterStateCustomActions(FSMTransitionMessage_Cube_Dash transitionMessage)
        {
            Debug.Log(_rollState.GetTimePassedInState());
            
            return _rollState.GetTimePassedInState() <= _rollStateTimeThreshold;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_Cube_Dash transitionMessage)
        {
            // undo roll 
            
            Debug.Log(transitionMessage.DashDirection);
            
            _dashMoveBehaviour.StartMovementTowards(
                _settings, TryGetDestination(transitionMessage.DashDirection, _dashDistance));
            
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
            // what should be next state??
        }
    }
}