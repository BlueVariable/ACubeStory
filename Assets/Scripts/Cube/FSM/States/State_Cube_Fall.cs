using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Fall : StateBase<ECubeState, FSMTransitionMessage_Cube_Roll>
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;
        
        public override ECubeState GetStateType()
        {
            return ECubeState.Fall;
        }

        protected override void EnterStateCustomActions(
            FSMTransitionMessage_Cube_Roll transitionMessage)
        {
            _cubeFSM.TryChangeState(ECubeState.Idle);
        }
    }
}