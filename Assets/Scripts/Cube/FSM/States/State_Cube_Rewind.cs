using UnityEngine;
using BLUE.FSMSystem;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Cube.src;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Rewind : StateBase<ECubeState, FSMTransitionMessage_Cube>
    {
        [SerializeField] private FSM_Cube _cubeFsm = null;
        
        [SerializeField] private RewindControllerBase _rewindController = null;
        
        [SerializeField] private ActionExecutor _stateEnteredActionExecutor = null;

        [SerializeField] private ActionExecutor _stateExitedActionExecutor = null;
        
        public override ECubeState GetStateType()
        {
            return ECubeState.Rewind;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_Cube transitionMessage)
        {
            _stateEnteredActionExecutor.Execute();

            _rewindController.OnRewindCompleted += OnRewindCompleted;
            
            _rewindController.TryRewind();

            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _stateExitedActionExecutor.Execute();
            
            base.ExitStateCustomActions();
        }

        private void OnRewindCompleted()
        {
            _rewindController.OnRewindCompleted -= OnRewindCompleted;

            _cubeFsm.TryChangeState(ECubeState.Fall);
        }
    }
}