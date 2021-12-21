using System;
using System.Collections.Generic;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Common;
using BLUE.ACubeStory.Cube;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class Trigger_CubeMergeableOverlap_Ground : Trigger_CubeMergeableOverlapBase, IUndoRollReasonProvider
    {
        [SerializeField] private MergeableComposition _mergeableComposition = null;

        [SerializeField] private State_Cube_Roll _cubeRollState = null;
        
        [SerializeField] private ActionExecutor _triggerAttemptedActionExecutor = null;

        public EUndoRollReason GetUndoRollReason => EUndoRollReason.CubeMergeableOverlap_Ground;
        
        private HashSet<Mergeable> _bottleneckMergeables = new HashSet<Mergeable>();
        
        #region Events

        protected override Action OnTriggerAttempt { get; set; }

        #endregion
        
        public override HashSet<Mergeable> GetLastBottleneckMergeables()
        {
            return _bottleneckMergeables;
        }

        protected override void ActivateCustomActions()
        {
            RegisterToCubeFSM();
            
            base.ActivateCustomActions();
        }

        protected override void DeactivateCustomActions()
        {
            UnregisterFromCubeFSM();

            UnregisterFromMergeableTriggers();
            
            base.DeactivateCustomActions();
        }

        private void RegisterToCubeFSM()
        {
            Cube.Cube.Instance.CubeFSM.OnStateEntered += OnCubeStateEntered;
        }
        
        private void UnregisterFromCubeFSM()
        {
            Cube.Cube.Instance.CubeFSM.OnStateEntered -= OnCubeStateEntered;
        }

        private void OnCubeStateEntered(ECubeState cubeState)
        {
            if (cubeState != ECubeState.Roll)
            {
                return;
            }
            
            _bottleneckMergeables.Clear();
            
            FSMTransitionMessage_Cube_Roll transitionMessage = _cubeRollState.LastCandidateTransitionMessage;

            if (transitionMessage == null
                || MergePuzzle.Instance == null)
            {
                return;
            }

            UnregisterFromMergeableTriggers();
            RegisterToMergeableTriggers();

            foreach (Mergeable mergedMergeable in _mergeableComposition.Mergeables)
            {
                if (mergedMergeable.WillOverlapGround(_mergeableComposition, transitionMessage.Direction.normalized))
                {
                    _bottleneckMergeables.Add(mergedMergeable);
                }
            }
        }

        private void RegisterToMergeableTriggers()
        {
            foreach (Mergeable mergedMergeable in _mergeableComposition.Mergeables)
            {
                Trigger_OnTriggerEnter enterTrigger = mergedMergeable.GetComponentInChildren<Trigger_OnTriggerEnter>();

                enterTrigger.OnTriggered += OnMergeableEnteredTrigger;
            }
        }
        
        private void UnregisterFromMergeableTriggers()
        {
            foreach (Mergeable mergedMergeable in _mergeableComposition.Mergeables)
            {
                Trigger_OnTriggerEnter enterTrigger = mergedMergeable.GetComponentInChildren<Trigger_OnTriggerEnter>();

                enterTrigger.OnTriggered -= OnMergeableEnteredTrigger;
            }
        }

        private void OnMergeableEnteredTrigger(TriggerBase trigger)
        {
            Trigger_OnTriggerEnter triggerEnter = ((Trigger_OnTriggerEnter) trigger);
            
            Mergeable mergeable = triggerEnter.GetComponentInParent<Mergeable>();
            Ground.Ground ground = triggerEnter.LastValidCollider.GetComponentInParent<Ground.Ground>();
            
            if (mergeable == null || ground == null)
            {
                return;
            }

            if (_bottleneckMergeables.Contains(mergeable))
            {
                OnTriggerAttempt?.Invoke();
                
                _triggerAttemptedActionExecutor.Execute();
            }
        }
    }
}
