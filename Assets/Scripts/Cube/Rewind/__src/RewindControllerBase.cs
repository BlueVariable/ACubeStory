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

            SaveSnapshotCustomActions(snapshot);
        }

        public bool CanRewind()
        {
            return !(_snapshots == null || _snapshots.Count == 0 || _curRewindSnapshot != null);
        }

        public bool IsThereAnyAvailableSnapshot()
        {
            return _snapshots != null && _snapshots.Count > 0;
        }

        public bool TryRewind()
        {
            if (_snapshots == null || _snapshots.Count == 0 || _curRewindSnapshot != null)
            {
                return false;
            }

            _curRewindSnapshot = _snapshots[_snapshots.Count - 1];

            _snapshots.RemoveAt(_snapshots.Count - 1);

            void onSnapshotRewindCompleted(RewindSnapshotBase snapshot)
            {
                _curRewindSnapshot = null;
                
                OnRewindCompleted?.Invoke(snapshot);
            }
            
            OnRewindStarted?.Invoke();
            
            _curRewindSnapshot.Rewind(onSnapshotRewindCompleted);
            
            TryRewindCustomActions();

            return true;
        }

        public bool TryRemoveSnapshot(T snapshot)
        {
            if (_snapshots[_snapshots.Count - 1] == snapshot)
            {
                _snapshots.RemoveAt(_snapshots.Count - 1);
                
                return true;
            }

            return false;
        }

        protected virtual void SaveSnapshotCustomActions(T snapshot) { }
        
        protected virtual void TryRewindCustomActions() { }
    }
}