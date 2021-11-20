using System;
using System.Collections.Generic;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Cube;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class Trigger_CubeMergeableOverlap_Ground : Trigger_CubeMergeableOverlapBase
    {
        [SerializeField] private MergeableComposition _mergeableComposition = null;

        [SerializeField] private State_Cube_Roll _cubeRollState = null;
        
        [SerializeField] private ActionExecutor _triggerAttemptedActionExecutor = null;

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
                ProcessGroundOverlap(mergedMergeable, transitionMessage.Direction.normalized);
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

        private void ProcessGroundOverlap(Mergeable mergeable, Vector3 normalizedDirection)
        {
            Vector3 mergedPos = mergeable.GetMergeablePos();
            mergedPos.Scale(normalizedDirection);
            
            Vector3 anchorPos = _mergeableComposition.GetAnchorMergeable(normalizedDirection).GetMergeablePos();
            anchorPos.Scale(normalizedDirection);

            Vector3 dir = mergedPos - anchorPos;

            float normalizedDirSum = normalizedDirection.x + normalizedDirection.y + normalizedDirection.z;

            float sum = (dir.x + dir.y + dir.z) * Mathf.Sign(normalizedDirSum);

            if (Mathf.RoundToInt(sum) > 0 && Mathf.Sign(normalizedDirSum) > 0)
            {
                _bottleneckMergeables.Add(mergeable);
            }
            else if (Mathf.RoundToInt(sum) < 0 && Mathf.Sign(normalizedDirSum) < 0)
            {
                _bottleneckMergeables.Add(mergeable);
            }
        }
    }
}
