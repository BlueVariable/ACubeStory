using _Common.Dash;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_DashJump : StateBase<ECubeState, FSMTransitionMessage_Cube_Dash>
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;
        
        [SerializeField] private DashBehaviour _dashBehaviour = null;
        
        public override ECubeState GetStateType()
        {
            return ECubeState.Dash_Jump;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_Cube_Dash transitionMessage)
        {
            _dashBehaviour.Dash(transitionMessage.DashDirection, () =>
            {
                _cubeFSM.TryChangeState(ECubeState.Fall);
            });
            
            base.EnterStateCustomActions(transitionMessage);
        }
    }
}