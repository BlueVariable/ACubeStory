using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public abstract class MergeSnapshotFactoryBase : MonoBehaviour
    {
        public abstract EMergeSnapshotType GetSnapshotType();
        
        public abstract RewindSnapshot_MergeComposition GetSnapshot();
    }
}