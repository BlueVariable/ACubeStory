using BLUE.Common;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Jump : StateBase<ECubeState, FSMTransitionMessage_Cube>
    {
        [Header("Object Settings")]
        [SerializeField] private FSM_Cube _cubeFSM = null;
        [SerializeField] private Transform _objectToJump = null;

        [Header("Jump Settings")]
        [SerializeField] private MoveBehaviour_Tween _moveBehaviour = null;
        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _moveSettings = null;

        [SerializeField] private float _jumpPower = 2f;
        
        public override ECubeState GetStateType()
        {
            return ECubeState.Jump;
        }

        protected override void EnterStateCustomActions(
            FSMTransitionMessage_Cube transitionMessage)
        {
            void onJumpCompleted()
            {
                _cubeFSM.TryChangeState(ECubeState.Fall);
            }
                    
            _moveBehaviour.StartMovementTowards(_moveSettings, 
                _objectToJump.position + new Vector3(0, _jumpPower, 0), 
                onJumpCompleted);
        }
    }
}