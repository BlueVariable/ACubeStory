using UnityEngine;
using System.Collections.Generic;

namespace BLUE.ACubeStory.Cube.src
{
    public class RewindControllerBase : MonoBehaviour
    {
        protected List<RewindableBase> _rewindables = new List<RewindableBase>();
        
        public void RegisterRewindable(RewindableBase rewindable)
        {
            if (_rewindables.Contains(rewindable))
            {
                return;
            }
            
            _rewindables.Add(rewindable);
        }

        public void UnregisterRewindable(RewindableBase rewindable)
        {
            _rewindables.Remove(rewindable);
        } 
    }
}