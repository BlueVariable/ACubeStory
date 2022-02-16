using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Roll : RewindSnapshot_Cube
    {
        public Vector3 RollDirection { get; }

        private CubeStateRewinder_Roll _rollRewinder;
        
        public RewindSnapshot_Cube_Roll(CubeStateRewinder_Roll rollRewinder, Vector3 rollDirection)
        {
            _rollRewinder = rollRewinder;
            RollDirection = rollDirection;
        }

        public override void Rewind(Action completedCallback)
        {
            _rollRewinder.RewindState(this, () =>
            {
                completedCallback?.Invoke();
            });
        }
    }
}