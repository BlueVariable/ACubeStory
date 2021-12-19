using BLUE.ActionSystem;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Rewind : StateBase<ECubeState, FSMTransitionMessage_Cube>
    {
        [SerializeField] private ActionExecutor _stateEnteredActionExecutor = null;

        [SerializeField] private ActionExecutor _stateExitedActionExecutor = null;
        
        public override ECubeState GetStateType()
        {
            return ECubeState.Rewind;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_Cube transitionMessage)
        {
            _stateEnteredActionExecutor.Execute();
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _stateExitedActionExecutor.Execute();
            
            base.ExitStateCustomActions();
        }
    }
}