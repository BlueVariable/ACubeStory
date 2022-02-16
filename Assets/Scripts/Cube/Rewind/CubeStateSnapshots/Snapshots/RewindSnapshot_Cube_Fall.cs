using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Fall : RewindSnapshot_Cube
    {
        public Transform CubeRootTransform { get; }
        
        public Vector3 FallStartPos { get; }
        
        private CubeStateRewinder_Fall _fallRewinder;

        public RewindSnapshot_Cube_Fall(CubeStateRewinder_Fall fallRewinder, Vector3 fallStartPos, Transform cubeRootTransform)
        {
            _fallRewinder = fallRewinder;
            FallStartPos = fallStartPos;
            CubeRootTransform = cubeRootTransform;
        }

        public override void Rewind(Action completedCallback)
        {
            _fallRewinder.RewindState(this, () =>
            {
                completedCallback?.Invoke();
            });
        }
    }
}