using System;
using System.Collections.Generic;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Cube;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class Trigger_CubeMergeableOverlap_Mergeable : Trigger_CubeMergeableOverlapBase
    {
        #region Constants

        private const float DISTANCE_TOLERANCE = 0.1f;

        #endregion
        
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
            UnregisterFromMergeableTriggers();
            
            UnregisterFromCubeFSM();
            
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

            Mergeable[] allMergeables = MergePuzzle.Instance.MergeableProvider.AllMergeables;

            foreach (Mergeable mergeable in allMergeables)
            {
                if (_mergeableComposition.Mergeables.Contains(mergeable))
                {
                    continue;
                }

                ProcessPreciseOverlap(mergeable, transitionMessage.Direction.normalized);
                ProcessVagueOverlap(mergeable, transitionMessage.Direction.normalized);
            }
        }
        
        private void RegisterToMergeableTriggers()
        {
            foreach (Mergeable mergedMergeable in MergePuzzle.Instance.MergeableProvider.AllMergeables)
            {
                Trigger_OnTriggerEnter enterTrigger = mergedMergeable.GetComponentInChildren<Trigger_OnTriggerEnter>();
                Trigger_OnTriggerStay stayTrigger = mergedMergeable.GetComponentInChildren<Trigger_OnTriggerStay>();

                enterTrigger.OnTriggered += OnMergeableEnteredTrigger;
                stayTrigger.OnTriggered += OnMergeableStayTrigger;
            }
        }
        
        private void UnregisterFromMergeableTriggers()
        {
            if (MergePuzzle.Instance == null)
            {
                return;
            }
            
            foreach (Mergeable mergedMergeable in MergePuzzle.Instance.MergeableProvider.AllMergeables)
            {
                Trigger_OnTriggerEnter enterTrigger = mergedMergeable.GetComponentInChildren<Trigger_OnTriggerEnter>();
                Trigger_OnTriggerStay stayTrigger = mergedMergeable.GetComponentInChildren<Trigger_OnTriggerStay>();

                enterTrigger.OnTriggered -= OnMergeableEnteredTrigger;
                stayTrigger.OnTriggered -= OnMergeableStayTrigger;
            }
        }

        private void OnMergeableEnteredTrigger(TriggerBase trigger)
        {
            Trigger_OnTriggerEnter triggerEnter = ((Trigger_OnTriggerEnter) trigger);
            
            Mergeable mergeable = triggerEnter.LastValidCollider.GetComponentInParent<Mergeable>();
            Mergeable selfMergeable = triggerEnter.GetComponentInParent<Mergeable>();
            
            if (selfMergeable == null || mergeable == null)
            {
                return;
            }

            if (_bottleneckMergeables.Contains(selfMergeable) && _bottleneckMergeables.Contains(mergeable))
            {
                OnTriggerAttempt?.Invoke();
                
                _triggerAttemptedActionExecutor.Execute();
            }
        }
        
        private void OnMergeableStayTrigger(TriggerBase trigger)
        {
            Trigger_OnTriggerStay triggerStay = ((Trigger_OnTriggerStay) trigger);
            
            Mergeable mergeable = triggerStay.LastValidCollider.GetComponentInParent<Mergeable>();
            Mergeable selfMergeable = triggerStay.GetComponentInParent<Mergeable>();
            
            if (selfMergeable == null || mergeable == null)
            {
                return;
            }

            if (_bottleneckMergeables.Contains(selfMergeable) && _bottleneckMergeables.Contains(mergeable))
            {
                OnTriggerAttempt?.Invoke();
                
                _triggerAttemptedActionExecutor.Execute();
            }
        }

        private void ProcessPreciseOverlap(Mergeable mergeable, Vector3 normalizedDirection)
        {
            Vector3 normalizedDirectionAbs = new Vector3(Mathf.Abs(normalizedDirection.x),
                Mathf.Abs(normalizedDirection.y), Mathf.Abs(normalizedDirection.z));
            
            Vector3 inversedDirection = new Vector3(normalizedDirectionAbs.z, 
                normalizedDirectionAbs.y, normalizedDirectionAbs.x);

            Vector3 overlapCandidateInversedPos = mergeable.GetMergeablePos();
            overlapCandidateInversedPos.Scale(inversedDirection);

            Vector3 overlapCandidateDefaultPos = mergeable.GetMergeablePos();
            overlapCandidateDefaultPos.Scale(normalizedDirectionAbs);
            
            foreach (Mergeable mergedMergeable in _mergeableComposition.Mergeables)
            {
                Vector3 mergedInversedPos = mergedMergeable.GetMergeablePos();
                mergedInversedPos.Scale(inversedDirection);

                Vector3 mergedDefaultPos = mergedMergeable.GetMergeablePos();
                mergedDefaultPos.Scale(normalizedDirectionAbs);

                float directionDistance = Vector3.Distance(overlapCandidateInversedPos, mergedInversedPos);

                if (directionDistance > DISTANCE_TOLERANCE)
                {
                    continue;
                }

                float normDirSum = normalizedDirection.x + normalizedDirection.y + normalizedDirection.z;
                
                Vector3 horDir = overlapCandidateDefaultPos - mergedDefaultPos;
                float horDirSum = horDir.x + horDir.y + horDir.z;

                if (Math.Abs(Mathf.Sign(normDirSum) - Mathf.Sign(horDirSum)) > DISTANCE_TOLERANCE)
                {
                    continue;
                }

                int horDistance = Mathf.RoundToInt(Vector3.Distance(overlapCandidateDefaultPos, mergedDefaultPos));

                int vertDistance = Mathf.RoundToInt(mergedMergeable.GetMergeablePos().y 
                                                    - mergeable.GetMergeablePos().y);
                
                vertDistance = Mathf.Abs(vertDistance);

                if (horDistance - 1 == vertDistance)
                {
                    _bottleneckMergeables.Add(mergeable);
                    _bottleneckMergeables.Add(mergedMergeable);
                }
            }
        }

        private void ProcessVagueOverlap(Mergeable mergeable, Vector3 normalizedDirection)
        {
            Vector3 normalizedDirectionAbs = new Vector3(Mathf.Abs(normalizedDirection.x),
                Mathf.Abs(normalizedDirection.y), Mathf.Abs(normalizedDirection.z));
            
            Vector3 inversedDirection = new Vector3(normalizedDirectionAbs.z, 
                normalizedDirectionAbs.y, normalizedDirectionAbs.x);
            
            Vector3 overlapCandidateInversedPos = mergeable.GetMergeablePos();
            overlapCandidateInversedPos.Scale(inversedDirection);
            
            foreach (Mergeable mergedMergeable in _mergeableComposition.Mergeables)
            {
                if (mergedMergeable.WillOverlapGround(_mergeableComposition, normalizedDirection))
                {
                    Vector3 mergedInversedPos = mergedMergeable.GetMergeablePos();
                    mergedInversedPos.Scale(inversedDirection);
                    
                    float directionDistance = Vector3.Distance(overlapCandidateInversedPos, mergedInversedPos);

                    if (directionDistance > DISTANCE_TOLERANCE)
                    {
                        continue;
                    }
                    
                    _bottleneckMergeables.Add(mergedMergeable);
                }
            }
        }
    }
}
