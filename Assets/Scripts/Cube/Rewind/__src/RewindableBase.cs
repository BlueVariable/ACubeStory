using System;
using UnityEngine;
using System.Collections.Generic;

namespace BLUE.ACubeStory.Cube.src
{
    public interface IRewindable
    {
        public bool TryRewind(Action undoCompleted);
        
        void ActivateRewindable(RewindController_CubeFSM rewindController);
        
        void DeactivateRewindable(RewindController_CubeFSM rewindController);
    }
    
    public abstract class RewindableBase<T> : MonoBehaviour, IRewindable
    where T : RewindSnapshotBase
    {
        private List<RewindSnapshotBase> _snapshots = new List<RewindSnapshotBase>();
        
        protected bool _isRewinding;

        public bool TryRewind(Action undoCompleted)
        {
            if (_isRewinding && _snapshots.Count == 0)
            {
                return false;
            }

            _isRewinding = true;

            RewindSnapshotBase snapshot = _snapshots[_snapshots.Count - 1];

            _snapshots.Remove(snapshot);
            
            snapshot.Rewind(OnSnapshotUndoCompleted);
            
            return true;
        }

        public void ActivateRewindable(RewindController_CubeFSM rewindController)
        {
            rewindController.RegisterRewindable(this);
        }

        public void DeactivateRewindable(RewindController_CubeFSM rewindController)
        {
            rewindController.UnregisterRewindable(this);
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