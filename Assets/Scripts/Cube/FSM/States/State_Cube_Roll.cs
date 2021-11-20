using System;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Common;
using BLUE.FSMSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Roll : StateBase<ECubeState, FSMTransitionMessage_Cube_Roll>
    {
        [Header("Object Settings")]
        [SerializeField] private FSM_Cube _cubeFSM = null;
        [SerializeField] private Transform _objectToRoll = null;
        
        [Header("Raycast Settings")]
        [SerializeField] private float _rollRayDistance = 1;
        [SerializeField] private LayerMask _layerMask = default;

        [Header("Roll Settings")] 
        [SerializeField] private RollBehaviourProviderBase _rollBehaviourProvider = null;

        [Header("Action Executors")] 
        [SerializeField] private ActionExecutor _rollCompletedActionExecutor = null;

        [SerializeField] private TriggerBase[] _undoRequestors = new TriggerBase[0];
        
        public FSMTransitionMessage_Cube_Roll LastCandidateTransitionMessage { get; private set; }

        public override ECubeState GetStateType()
        {
            return ECubeState.Roll;
        }
        
        protected override bool CanEnterStateCustomActions(FSMTransitionMessage_Cube_Roll transitionMessage)
        {
            LastCandidateTransitionMessage = transitionMessage;
            
            return !Physics.Raycast(_objectToRoll.position, transitionMessage.Direction,
                out RaycastHit hit, _rollRayDistance, _layerMask);
        }
        
        protected override void EnterStateCustomActions(
            FSMTransitionMessage_Cube_Roll transitionMessage)
        {
            _rollBehaviourProvider.GetRollBehaviour().TryRollTo(
                transitionMessage.Direction, 
                onRolledCallback: OnRollCompleted);

            RegisterToUndoRequestors();
        }

        protected override void ExitStateCustomActions()
        {
            _rollBehaviourProvider.GetRollBehaviour().TryPauseRoll();
            
            base.ExitStateCustomActions();
        }

        private void RegisterToUndoRequestors()
        {
            Array.ForEach(_undoRequestors, i => i.OnTriggered += OnUndoRequested);
        }

        private void UnregisterFromUndoRequestors()
        {
            Array.ForEach(_undoRequestors, i => i.OnTriggered -= OnUndoRequested);
        }

        private void OnRollCompleted()
        {
            UnregisterFromUndoRequestors();
                
            _rollCompletedActionExecutor.Execute();
                
            _cubeFSM.TryChangeState(ECubeState.Fall);
        }

        private void OnUndoRequested(TriggerBase triggerBase)
        {
            UnregisterFromUndoRequestors();
            
            _rollBehaviourProvider.GetRollBehaviour().TryUndoRoll(
                onRolledCallback: OnRollCompleted);
        }
    }
}