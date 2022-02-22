using System;
using UnityEngine;
using BLUE.ACubeStory.Common;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Roll : RewindSnapshot_Cube
    {
        public Vector3 RollDirection { get; }
        
        public float RollSpeed { get; }
        
        public float RollAngle { get; private set; }
        
        public AnimationCurve RollCurve { get; }
        
        public Vector3 RollPointOffset { get; }
        
        private CubeSnapshotRewinder_Roll _rollRewinder;

        public RewindSnapshot_Cube_Roll(
            CubeSnapshotRewinder_Roll rollRewinder,
            Vector3 rollDirection,
            float rollSpeed,
            float rollAngle,
            AnimationCurve rollCurve,
            Vector3 rollPointOffset)
        {
            _rollRewinder = rollRewinder;
            RollDirection = rollDirection;
            RollSpeed = rollSpeed;
            RollAngle = rollAngle;
            RollCurve = rollCurve;
            RollPointOffset = rollPointOffset;
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