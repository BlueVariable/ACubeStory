using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Climb : RewindSnapshot_Cube
    {
        public Vector3 ClimbDirection { get; set; }

        private CubeStateRewinder_Climb _climbRewinder = null;

        public RewindSnapshot_Cube_Climb(CubeStateRewinder_Climb climbRewinder, Vector3 climbDirection)
        {
            _climbRewinder = climbRewinder;
            ClimbDirection = climbDirection;
        }

        public override void Rewind(Action completedCallback)
        {
            _climbRewinder.RewindState(this, () =>
            {
                completedCallback?.Invoke();
            });
        }
    }
}