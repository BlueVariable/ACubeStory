using System;
using System.Linq;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Common;
using BLUE.Common;
using BLUE.FSMSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Roll : StateBase<ECubeState, FSMTransitionMessage_Cube_Roll>
    {
        [Serializable]
        private class UndoReasonActionExecutorInfo
        {
            [SerializeField] private EUndoRollReason _undoRollReasonType = EUndoRollReason.None;
            public EUndoRollReason UndoRollReasonType => _undoRollReasonType;

            [SerializeField] private ActionExecutor _undoRollStartedActionExecutor = null;
            public ActionExecutor UndoRollStartedActionExecutor => _undoRollStartedActionExecutor;
        }
        
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
        [SerializeField] private UndoReasonActionExecutorInfo[] _undoReasonActionExecutorMap = default;

        [Header("Triggers")]
        [SerializeField] private TriggerBase[] _undoRequestors = new TriggerBase[0];

        [Header("Conditions")] 
        [SerializeField] private SimpleConditionBase[] _forceEnterConditions = null;
        
        public FSMTransitionMessage_Cube_Roll LastCandidateTransitionMessage { get; private set; }
        
        public override ECubeState GetStateType()
        {
            return ECubeState.Roll;
        }
        
        protected override bool CanEnterStateCustomActions(FSMTransitionMessage_Cube_Roll transitionMessage)
        {
            LastCandidateTransitionMessage = transitionMessage;

            if (_forceEnterConditions.Any(val => val.IsConditionSatisfied()))
            {
                return true;
            }
            
            return !Physics.Raycast(_objectToRoll.position, transitionMessage.Direction,
                out RaycastHit hit, _rollRayDistance, _layerMask);
        }
        
        protected override void EnterStateCustomActions(
            FSMTransitionMessage_Cube_Roll transitionMessage)
        {
            RegisterToUndoRequestors();

            if (transitionMessage.Speed != 0f)
            {
                _rollBehaviourProvider.GetRollBehaviour().TryRollTo(
                    transitionMessage.Direction,
                    speed: transitionMessage.Speed,
                    onRolledCallback: OnRollCompleted);
            }
            else
            {
                _rollBehaviourProvider.GetRollBehaviour().TryRollTo(
                    transitionMessage.Direction, 
                    onRolledCallback: OnRollCompleted);
            }
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

            if (_rollBehaviourProvider.GetRollBehaviour().TryUndoRoll(
                    onRolledCallback: OnRollCompleted))
            {
                if (triggerBase is IUndoRollReasonProvider reasonProvider)
                {
                    UndoReasonActionExecutorInfo info = _undoReasonActionExecutorMap.FirstOrDefault(val =>
                        val.UndoRollReasonType == reasonProvider.GetUndoRollReason);

                    if (info != null)
                    {
                        info.UndoRollStartedActionExecutor.Execute();
                    }
                }
            }
        }
    }
}