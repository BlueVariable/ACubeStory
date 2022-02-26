using UnityEngine;
using BLUE.FSMSystem;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Cube.src;
using BLUE.Helpers;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Rewind : StateBase<ECubeState, FSMTransitionMessage_Cube>
    {
        [SerializeField] private FSM_Cube _cubeFsm = null;
        
        [SerializeField] private RewindController_Cube _rewindController = null;
        
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

        private void OnRewindCompleted(RewindSnapshotBase snapshot)
        {
            _rewindController.OnRewindCompleted -= OnRewindCompleted;

            if (!(snapshot is RewindSnapshot_Cube snapshotCube))
            {
                return;
            }

            _cubeFsm.TryChangeState(ECubeState.Idle);
            
            if (snapshotCube.CubeState == ECubeState.Fall)
            {
                CoroutineHelper.Instance.WaitForSeconds(1f / 60f,() =>
                {
                    if (_cubeFsm.TryGetCurStateType(out ECubeState curState) && curState != ECubeState.Rewind)
                    {
                        _cubeFsm.TryChangeState(ECubeState.Fall);
                    }
                });
            }
        }
    }
}