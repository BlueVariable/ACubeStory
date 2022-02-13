using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube.StateRewinder
{
    public abstract class CubeStateRewinderBase : MonoBehaviour
    {
        public abstract ECubeState GetRewinderState();
        
        public abstract void RewindState(Action onCompleted);
    }
}