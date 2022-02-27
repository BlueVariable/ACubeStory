using System;
using UnityEngine;
using System.Collections.Generic;

namespace BLUE.ACubeStory.Cube.src
{
    public abstract class RewindControllerBase<T> : MonoBehaviour 
    where T : RewindSnapshotBase
    {
        protected List<T> _snapshots = new List<T>();

        protected T _curRewindSnapshot = null;

        public Action OnRewindStarted { get; set; }
        
        public Action<RewindSnapshotBase> OnRewindCompleted { get; set; }
        
        public void SaveSnapshot(T snapshot)
        {
            _snapshots.Add(snapshot);
        }

        public bool CanRewind()
        {
            return !(_snapshots == null || _snapshots.Count == 0 || _curRewindSnapshot != null);
        }

        public void TryRewind()
        {
            if (_snapshots == null || _snapshots.Count == 0 || _curRewindSnapshot != null)
            {
                return;
            }

            _curRewindSnapshot = _snapshots[_snapshots.Count - 1];

            _snapshots.RemoveAt(_snapshots.Count - 1);

            void onSnapshotRewindCompleted(RewindSnapshotBase snapshot)
            {
                OnRewindCompleted?.Invoke(_curRewindSnapshot);
                
                _curRewindSnapshot = null;
            }
            
            _curRewindSnapshot.Rewind(onSnapshotRewindCompleted);
            
            OnRewindStarted?.Invoke();
        }
    }
}