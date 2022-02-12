using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.Cube.src
{
    public class RewindableBase : MonoBehaviour
    {
        private List<RewindSnapshotBase> _snapshots = new List<RewindSnapshotBase>();

        public void SaveSnapshot()
        {
            
        }

        public void UndoSnapshot()
        {
            
        }
    }
}