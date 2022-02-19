using System;
using UnityEngine;
using System.Collections.Generic;

namespace BLUE.ACubeStory.Cube.src
{
    public interface IRewindable
    {
        public bool TryRewind(Action onRewindCompleted);
        
        void ActivateRewindable(RewindController_CubeMovement rewindController);
        
        void DeactivateRewindable(RewindController_CubeMovement rewindController);
    }
    
    public abstract class RewindableBase<T> : MonoBehaviour, IRewindable
    where T : RewindSnapshotBase
    {
        private List<RewindSnapshotBase> _snapshots = new List<RewindSnapshotBase>();
        
        protected bool _isRewinding;

        private Action _onRewindCompleted;

        public bool TryRewind(Action onRewindCompleted)
        {
            if (_isRewinding || _snapshots.Count == 0)
            {
                return false;
            }

            _onRewindCompleted = onRewindCompleted;

            _isRewinding = true;

            RewindSnapshotBase snapshot = _snapshots[_snapshots.Count - 1];

            _snapshots.Remove(snapshot);
            
            snapshot.Rewind(OnRewindCompleted);
            
            return true;
        }

        public void ActivateRewindable(RewindController_CubeMovement rewindController)
        {
            rewindController.RegisterRewindable(this);
        }

        public void DeactivateRewindable(RewindController_CubeMovement rewindController)
        {
            rewindController.UnregisterRewindable(this);
        }

        private void OnRewindCompleted()
        {
            _isRewinding = false;
            
            _onRewindCompleted?.Invoke();
        }
        
        protected void SaveSnapshot(T snapshot)
        {
            _snapshots.Add(snapshot);
        }
    }
}