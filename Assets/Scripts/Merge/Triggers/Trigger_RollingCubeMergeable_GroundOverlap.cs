using System;
using System.Collections.Generic;
using BLUE.ACubeStory.Cube;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class Trigger_RollingCubeMergeable_GroundOverlap : TriggerBase
    {
        [SerializeField] private MergeableComposition _mergeableComposition = null;

        [SerializeField] private State_Cube_Roll _cubeRollState = null;

        private HashSet<Mergeable> _bottleneckMergeables = new HashSet<Mergeable>();
        
        #region Events

        protected override Action OnTriggerAttempt { get; set; }

        #endregion

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
                Trigger_OnTriggerEnter trigger = mergedMergeable.GetComponentInChildren<Trigger_OnTriggerEnter>();

                trigger.OnTriggered += OnMergeableEnteredTrigger;
            }
        }
        
        private void UnregisterFromMergeableTriggers()
        {
            foreach (Mergeable mergedMergeable in _mergeableComposition.Mergeables)
            {
                Trigger_OnTriggerEnter trigger = mergedMergeable.GetComponentInChildren<Trigger_OnTriggerEnter>();

                trigger.OnTriggered -= OnMergeableEnteredTrigger;
            }
        }

        private void OnMergeableEnteredTrigger(TriggerBase trigger)
        {
            Mergeable mergeable = ((Trigger_OnTriggerEnter) trigger).GetComponentInParent<Mergeable>();
            
            if (mergeable == null)
            {
                return;
            }

            if (_bottleneckMergeables.Contains(mergeable))
            {
                OnTriggerAttempt?.Invoke();
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
