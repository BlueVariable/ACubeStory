using System;
using UnityEngine;
using System.Collections.Generic;

namespace BLUE.ACubeStory.Cube.src
{
    public abstract class RewindControllerBase : MonoBehaviour
    {
        protected List<IRewindable> _rewindables = new List<IRewindable>();

        protected List<IRewindable> _activeRewindables = new List<IRewindable>();
        
        public Action OnRewindStarted { get; set; }
        
        public Action<RewindSnapshotBase> OnRewindCompleted { get; set; }
            
        public void RegisterRewindable(IRewindable rewindable)
        {
            if (_rewindables.Contains(rewindable))
            {
                return;
            }
            
            _rewindables.Add(rewindable);
        }

        public void UnregisterRewindable(IRewindable rewindable)
        {
            _rewindables.Remove(rewindable);
        }

        public void TryRewind()
        {
            bool isActiveRewindablesClearAtInit = _activeRewindables.Count == 0;

            bool isThereAnyNewRewindable = false;
            
            foreach (IRewindable rewindable in _rewindables)
            {
                void onRewindCompleted(RewindSnapshotBase snapshot)
                {
                    _activeRewindables.Remove(rewindable);
                    
                    if (_activeRewindables.Count == 0)
                    {
                        OnRewindCompleted?.Invoke(snapshot);
                    }
                }
                
                if (rewindable.TryRewind(onRewindCompleted))
                {
                    _activeRewindables.Add(rewindable);

                    isThereAnyNewRewindable = true;
                }
            }

            if (isActiveRewindablesClearAtInit && isThereAnyNewRewindable)
            {
                OnRewindStarted?.Invoke();
            }
        }
    }
}