using UnityEngine;
using System.Collections.Generic;

namespace BLUE.ACubeStory.Cube.src
{
    public class RewindControllerBase : MonoBehaviour
    {
        protected List<IRewindable> _rewindables = new List<IRewindable>();

        protected List<IRewindable> _activeRewindables = new List<IRewindable>();

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
            foreach (IRewindable rewindable in _rewindables)
            {
                void onUndoCompleted()
                {
                    _activeRewindables.Remove(rewindable);
                }
                
                if (rewindable.TryRewind(onUndoCompleted))
                {
                    _activeRewindables.Add(rewindable);
                }
            }
        }
    }
}