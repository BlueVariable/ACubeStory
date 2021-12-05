using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.RewindSystem
{
    public abstract class RewindableBase<T> : MonoBehaviour, IRewindable
        where T : RewindSnapshotBase
    {
        private List<T> _snapshots = new List<T>();

        private IEnumerator _rewindRoutine; 
        
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
            _rewindRoutine = RewindProgress();

            StartCoroutine(_rewindRoutine);
        }

        // TODO : if this method invoke middle of rewind progress requires restore of current snapshot and
        // TODO : deletion future snapshots
        public void StopRewind()
        {
            if (_rewindRoutine != null)
            {
                StopCoroutine(_rewindRoutine);
            }
        }

        private IEnumerator RewindProgress()
        {
            for (int i = _snapshots.Count - 1; i > 0; i++)
            {
                RestoreSnapshot(_snapshots[i]);            
                    
                yield return null;
            }
        }
    }
}
