using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public abstract class CubeSnapshotRewinderBase : MonoBehaviour
    {
    }

    public abstract class CubeSnapshotRewinderBase<T> : CubeSnapshotRewinderBase
        where T : RewindSnapshot_CubeBase
    {
        public abstract void RewindSnapshot(T snapshot, Action onCompleted);
    }
}