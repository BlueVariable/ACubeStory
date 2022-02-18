using System;
using UnityEngine;
using BLUE.FSMSystem;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Cube.src;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Rewind : StateBase<ECubeState, FSMTransitionMessage_Cube>
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;
        
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
            
            base.EnterStateCustomActions(transitionMessage);
        }

        protected override void ExitStateCustomActions()
        {
            _stateExitedActionExecutor.Execute();
            
            base.ExitStateCustomActions();
        }

        private void Awake()
        {
            RegisterToRewindController();
        }

        private void OnDestroy()
        {
            UnregisterFromRewindController();
        }

        private void RegisterToRewindController()
        {
            _rewindController.OnRewindStarted += OnRewindStarted;
            _rewindController.OnRewindCompleted += OnRewindCompleted;
        }

        private void UnregisterFromRewindController()
        {
            _rewindController.OnRewindStarted -= OnRewindStarted;
            _rewindController.OnRewindCompleted -= OnRewindCompleted;
        }

        private void OnRewindStarted()
        {
            _cubeFSM.TryChangeState(ECubeState.Rewind);
        }

        private void OnRewindCompleted()
        {
            _cubeFSM.TryChangeState(ECubeState.Idle);
        }
    }
}