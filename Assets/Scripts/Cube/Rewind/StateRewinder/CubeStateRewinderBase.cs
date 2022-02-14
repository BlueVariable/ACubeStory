using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube.StateRewinder
{
    public abstract class CubeStateRewinderBase : MonoBehaviour
    {
        public abstract ECubeState GetRewinderState();
    }

    public abstract class CubeStateRewinderBase<T> : CubeStateRewinderBase
    where T : RewindSnapshot_Cube
    {
        public abstract void RewindState(T snapshot, Action onCompleted);
    }
}