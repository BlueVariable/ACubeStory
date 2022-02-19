using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube.StateRewinder
{
    public abstract class CubeSnapshotRewinder : MonoBehaviour
    {
    }

    public abstract class CubeSnapshotRewinder<T> : CubeSnapshotRewinder
    where T : RewindSnapshot_Cube
    {
        public abstract void RewindState(T snapshot, Action onCompleted);
    }
}