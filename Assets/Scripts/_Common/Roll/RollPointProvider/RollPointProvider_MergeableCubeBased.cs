using System;
using System.Collections;
using System.Linq;
using BLUE.ACubeStory.Cube;
using BLUE.ACubeStory.Merge;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class RollPointProvider_MergeableCubeBased : RollPointProviderBase
    {
        [Serializable]
        private class Settings
        {
            [SerializeField] private Vector3 _direction = Vector3.zero;
            public Vector3 Direction => _direction;

            [SerializeField] private Vector3 _offset = default;
            public Vector3 Offset => _offset;
        }
        
        #region Constants

        private const float HEIGHT_TOLERANCE = 0.05f;

        #endregion
        
        [SerializeField] private Settings[] _settings = new Settings[0];
        
        [SerializeField] private MergeableComposition _mergeableComposition = null;

        [SerializeField] private FSM_Cube _cubeFSM = null;

        private Action<Vector3> _onRollPointUpdatedCallback = null;
        
        private IEnumerator _checkMergeableHitGroundRoutine;
        
        public override Vector3 GetRollPoint(Vector3 direction, Action<Vector3> onRollPointUpdatedCallback = null)
        {
            _onRollPointUpdatedCallback = onRollPointUpdatedCallback;

            return GetDefaultRollPoint(direction);
        }

        protected override void AttachedCustomActions()
        {
            RegisterToCubeFSM();
            
            base.AttachedCustomActions();
        }

        protected override void DetachedCustomActions()
        {
            StopCheckMergeableHitGroundRoutine();
            
            UnregisterFromCubeFSM();
            
            base.DetachedCustomActions();
        }

        private void OnDestroy()
        {
            StopCheckMergeableHitGroundRoutine();
            
            UnregisterFromCubeFSM();
        }

        private Vector3 GetDefaultRollPoint(Vector3 direction)
        {
            Mergeable targetMergeable = _mergeableComposition.Mergeables.ElementAt(0);

            foreach (Mergeable mergeable in _mergeableComposition.Mergeables)
            {
                if (mergeable.GetMergeablePos().y < targetMergeable.GetMergeablePos().y - HEIGHT_TOLERANCE)
                {
                    targetMergeable = mergeable;

                    continue;
                }

                if (mergeable.GetMergeablePos().y > targetMergeable.GetMergeablePos().y + HEIGHT_TOLERANCE)
                {
                    continue;
                }
                
                Vector3 prev = targetMergeable.GetMergeablePos();
                Vector3 cur = mergeable.GetMergeablePos();
                
                prev.Scale(direction);
                cur.Scale(direction);

                if (cur.x + cur.y + cur.z > prev.x + prev.y + prev.z)
                {
                    targetMergeable = mergeable;
                }
            }

            Vector3 rollPoint = targetMergeable.GetMergeablePos() 
                                + _settings.FirstOrDefault(i => i.Direction.Equals(direction)).Offset;

            return rollPoint;
        }

        private void RegisterToCubeFSM()
        {
            _cubeFSM.OnStateEntered += OnStateEntered;
            _cubeFSM.OnStateExited += OnStateExited;
        }
        
        private void UnregisterFromCubeFSM()
        {
            _cubeFSM.OnStateEntered -= OnStateEntered;
            _cubeFSM.OnStateExited -= OnStateExited;
        }

        private void OnStateEntered(ECubeState cubeState)
        {
            if (cubeState == ECubeState.Roll)
            {
                if (_mergeableComposition.Mergeables.Count <= 1)
                {
                    return;
                }
                
                StartCheckMergeableHitGroundRoutine();
            }
        }
        
        private void OnStateExited(ECubeState cubeState)
        {
            if (cubeState == ECubeState.Roll)
            {
                StopCheckMergeableHitGroundRoutine();
            }
        }

        private void StartCheckMergeableHitGroundRoutine()
        {
            StopCheckMergeableHitGroundRoutine();

            _checkMergeableHitGroundRoutine = CheckMergeableHitGroundProgress();

            StartCoroutine(_checkMergeableHitGroundRoutine);
        }
        
        private void StopCheckMergeableHitGroundRoutine()
        {
            if (_checkMergeableHitGroundRoutine != null)
            {
                StopCoroutine(_checkMergeableHitGroundRoutine);
            }
        }

        private IEnumerator CheckMergeableHitGroundProgress()
        {
            Mergeable anchorMergeable = _mergeableComposition.Mergeables.OrderBy(val 
                => val.GetMergeablePos().y).First();

            Bounds anchorBounds = anchorMergeable.Collider.bounds;

            Vector3 anchorPos = anchorBounds.ClosestPoint(anchorMergeable.GetMergeablePos() - 
                                                          Vector3.up * anchorBounds.size.y);

            while (true)
            {
                foreach (Mergeable mergeable in _mergeableComposition.Mergeables)
                {
                    Bounds candidateBounds = mergeable.Collider.bounds;

                    Vector3 candidatePos = candidateBounds.ClosestPoint(mergeable.GetMergeablePos() - 
                                                                        Vector3.up * anchorBounds.size.y);

                    if (candidatePos.y < anchorPos.y - HEIGHT_TOLERANCE)
                    {
                        _onRollPointUpdatedCallback?.Invoke(candidatePos);

                        yield break;
                    }
                }

                yield return null;
            }
        }
    }
}