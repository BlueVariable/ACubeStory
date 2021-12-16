using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.Rewind
{
    public abstract class RewindableBase<T> : MonoBehaviour, IRewindable
        where T : RewindSnapshotBase
    {
        private List<T> _snapshots = new List<T>();

        private IEnumerator _rewindRoutine;

        private int _curSnapshotIndex = 0;

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
        
        public void SaveSnapshot()
        {
            _snapshots.Add(GetSnapshot());
        }

        public void ResetSavedSnapshots()
        {
            _snapshots.Clear();
        }

        public void StartRewind()
        {
            _curSnapshotIndex = _snapshots.Count - 1;
            
            StopRewind();
            
            _rewindRoutine = RewindProgress();

            StartCoroutine(_rewindRoutine);
        }
        
        public void StopRewind()
        {
            if (_rewindRoutine != null)
            {
                StopCoroutine(_rewindRoutine);
            }

            int count = _snapshots.Count;
            
            for (int i = count - 1; i >= _curSnapshotIndex; i--)
            {
                _snapshots.RemoveAt(i);
            }
        }

        private IEnumerator RewindProgress()
        {
            for (int i = _snapshots.Count - 1; i > 0; i--)
            {
                _curSnapshotIndex = i;

                Debug.Log(_curSnapshotIndex);
                
                RestoreSnapshot(_snapshots[i]);            
                    
                yield return null;
            }

            OnRewound?.Invoke(this);
        }
    }
}
