using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Jump : RewindSnapshot_Cube
    {
        public Vector3 JumpStartPosition { get; }

        private CubeStateRewinder_Jump _jumpRewinder;

        public RewindSnapshot_Cube_Jump(CubeStateRewinder_Jump jumpRewinder, Vector3 jumpStartPosition)
        {
            _jumpRewinder = jumpRewinder;
            JumpStartPosition = jumpStartPosition;
        }

        public override void Rewind(Action completedCallback)
        {
            _jumpRewinder.RewindState(this, () =>
            {
                completedCallback?.Invoke();
            });
        }
    }
}