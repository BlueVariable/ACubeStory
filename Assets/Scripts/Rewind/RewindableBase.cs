using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BLUE.ACubeStory.Rewind
{
    public abstract class RewindableBase<T> : MonoBehaviour, IRewindable
        where T : RewindSnapshotBase
    {
        private List<T> _snapshots = new List<T>();

        private IEnumerator _rewindRoutine;

        private int _curSnapshotIndex = 0;

        private bool _isRewindStarted = false;

        private int _curIdenticalSnapshotCount = 0;

        #region Events

        public Action<IRewindable> OnRewound { get; set; }

        #endregion

        protected abstract T GetSnapshot();

        protected abstract void RestoreSnapshot(T snapshot);

        public void ActivateRewindable(RewindController rewindController)
        {
            rewindController.RegisterRewindable(this);
        }
        
        public void DeactivateRewindable(RewindController rewindController)
        {
            rewindController.UnregisterRewindable(this);
        }
        
        public bool IsIdenticalSnapshotLimitReached(int limit)
        {
            T snapshot = GetSnapshot();

            if (limit < 0)
            {
                return false;
            }

            bool isIdentical = false;
            
            if (_snapshots.Count > 0)
            {
                isIdentical = snapshot.IsIdentical(_snapshots[_snapshots.Count - 1]);
            }

            if (!isIdentical)
            {
                return false;
            }

            if (_curIdenticalSnapshotCount <= limit)
            {
                return false;
            }

            return true;
        }

        public bool TrySaveSnapshot()
        {
            T snapshot = GetSnapshot();
            
            if (_snapshots.Count > 0 && snapshot.IsIdentical(_snapshots[_snapshots.Count - 1]))
            {
                _curIdenticalSnapshotCount++;
            }

            if (_snapshots.Count > 0 && !snapshot.IsIdentical(_snapshots[_snapshots.Count - 1]))
            {
                _curIdenticalSnapshotCount = 0;
            }

            _snapshots.Add(snapshot);

            return true;
        }

        public void ResetSavedSnapshots()
        {
            _snapshots.Clear();
        }

        public void StartRewind()
        {
            _curSnapshotIndex = _snapshots.Count - 1;

            _curSnapshotIndex = Mathf.Clamp(_curSnapshotIndex, 0, _snapshots.Count);
            
            StopRewind();
            
            _rewindRoutine = RewindProgress();

            StartCoroutine(_rewindRoutine);

            _isRewindStarted = true;
        }
        
        public void StopRewind()
        {
            if (_rewindRoutine != null)
            {
                StopCoroutine(_rewindRoutine);
            }

            if (!_isRewindStarted)
            {
                return;
            }

            int count = _snapshots.Count;
            
            for (int i = count - 1; i >= _curSnapshotIndex; i--)
            {
                _snapshots.RemoveAt(i);
            }

            _isRewindStarted = false;
        }

        private IEnumerator RewindProgress()
        {
            for (int i = _snapshots.Count - 1; i >= 0; i--)
            {
                _curSnapshotIndex = i;

                RestoreSnapshot(_snapshots[i]);            
                    
                yield return null;
            }

            _isRewindStarted = false;

            ResetSavedSnapshots();
            
            OnRewound?.Invoke(this);
        }
    }
}
