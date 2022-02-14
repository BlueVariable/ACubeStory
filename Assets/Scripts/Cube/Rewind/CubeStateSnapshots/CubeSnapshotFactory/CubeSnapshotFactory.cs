using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public abstract class CubeSnapshotFactory : MonoBehaviour
    {
        public abstract ECubeState GetCubeState();
        
        public abstract RewindSnapshot_Cube CreateSnapshot();
    }
}