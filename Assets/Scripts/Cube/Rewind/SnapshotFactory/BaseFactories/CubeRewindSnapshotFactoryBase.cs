using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public abstract class CubeRewindSnapshotFactoryBase : MonoBehaviour
    {
        public abstract ECubeState GetCubeState();
        
        public abstract bool CreateSnapshot(Action<RewindSnapshot_CubeBase> createdCallback);
    }
}