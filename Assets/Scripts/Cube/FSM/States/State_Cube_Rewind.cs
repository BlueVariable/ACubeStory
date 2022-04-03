using System.Linq;
using UnityEngine;
using BLUE.FSMSystem;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Common;
using BLUE.ACubeStory.Cube.src;
using BLUE.Helpers;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Rewind : StateBase<ECubeState, FSMTransitionMessage_Cube>
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;
        
        [SerializeField] private RewindController_Cube _rewindController = null;

        [SerializeField] private RollBehaviourBase _rollBehaviour = null;
        
        [SerializeField] private ActionExecutor _stateEnteredActionExecutor = null;

        [SerializeField] private ActionExecutor _stateExitedActionExecutor = null;

        [SerializeField] private ECubeState[] _recursiveTransitionStates = default;
        [SerializeField] private float _recursiveTransitionHandleDelay = 0.015f;
        
        private bool _canContinueRewind = false;

        public override ECubeState GetStateType()
        {
            return ECubeState.Rewind;
        }

        protected override bool CanEnterStateCustomActions(FSMTransitionMessage_Cube transitionMessage)
        {
            return _rewindController.CanRewind() && !_rollBehaviour.IsUndoing;
        }

        protected override void EnterStateCustomActions(FSMTransitionMessage_Cube transitionMessage)
        {
            _canContinueRewind = true;
            
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
        
        public void SetCanContinueRewind(bool val)
        {
            _canContinueRewind = val;
        }

        private void OnRewindCompleted(RewindSnapshotBase snapshot)
        {
            if (_canContinueRewind && _rewindController.TryRewind())
            {
                return;
            }
            
            _rewindController.OnRewindCompleted -= OnRewindCompleted;

            if (!(snapshot is RewindSnapshot_Cube snapshotCube))
            {
                return;
            }

            _cubeFSM.TryChangeState(ECubeState.Idle);
            
            if (_recursiveTransitionStates.Contains(snapshotCube.CubeState))
            {
                HandleRecursiveTransition();
            }
        }

        private void HandleRecursiveTransition()
        {
            void onDelayCompleted()
            {
                if (_cubeFSM.TryGetCurStateType(out ECubeState curState) && curState != ECubeState.Rewind)
                {
                    _cubeFSM.TryChangeState(ECubeState.Fall);
                }
            }

            CoroutineHelper.Instance.WaitForSeconds(_recursiveTransitionHandleDelay, onDelayCompleted);
        }
    }
}