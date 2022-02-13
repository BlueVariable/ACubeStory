using System;
using UnityEngine;
using System.Collections.Generic;

namespace BLUE.ACubeStory.Cube.src
{
    public interface IRewindable
    {
        public bool TryRewind(Action undoCompleted);
    }
    
    public abstract class RewindableBase<T> : MonoBehaviour, IRewindable
    where T : RewindSnapshotBase
    {
        private List<RewindSnapshotBase> _snapshots = new List<RewindSnapshotBase>();
        
        protected bool _isRewinding;

        public bool TryRewind(Action undoCompleted)
        {
            if (_isRewinding)
            {
                return false;
            }

            _isRewinding = true;

            RewindSnapshotBase snapshot = _snapshots[_snapshots.Count - 1];

            _snapshots.Remove(snapshot);
            
            snapshot.Undo(OnSnapshotUndoCompleted);
            
            return true;
        }

        private void OnSnapshotUndoCompleted()
        {
            _isRewinding = false;
        }
        
        protected void SaveSnapshot(T snapshot)
        {
            _snapshots.Add(snapshot);
        }
    }
}