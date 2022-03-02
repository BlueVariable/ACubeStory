using System;
using BLUE.ACubeStory.Common;
using UnityEngine;
using BLUE.ACubeStory.Cube.src;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Roll : RewindSnapshot_Cube_Platform
    {
        public Vector3 RollDirection { get; }
        
        public float RollSpeed { get; }
        
        public float RollAngle { get; private set; }
        
        public AnimationCurve RollCurve { get; }
        
        public Vector3 RollPointOffset { get; }
        
        private CubeSnapshotRewinder_Roll _rollRewinder;

        private RollBehaviourBase _rollBehaviour;

        private bool _isCurrentRollCompleted;

        public RewindSnapshot_Cube_Roll(
            ECubeState cubeState,
            CubeSnapshotRewinder_Roll rollRewinder,
            RollBehaviourBase rollBehaviour,
            Vector3 rollDirection,
            float rollSpeed,
            float rollAngle,
            AnimationCurve rollCurve,
            Vector3 rollPointOffset)
        : base(cubeState)
        {
            _rollRewinder = rollRewinder;
            _rollBehaviour = rollBehaviour;
            RollDirection = rollDirection;
            RollSpeed = rollSpeed;
            RollAngle = rollAngle;
            RollCurve = rollCurve;
            RollPointOffset = rollPointOffset;

            _rollBehaviour.OnRollEnded += OnRollEnded;

            _isCurrentRollCompleted = false;
        }

        public override void Rewind(Action<RewindSnapshotBase> completedCallback)
        {
            if (_isCurrentRollCompleted)
            {
                _rollRewinder.RewindState(this, () => { completedCallback?.Invoke(this); });
            }
            else
            {
                _rollBehaviour.TryUndoRoll(true,() => { completedCallback?.Invoke(this); });
            }
        }

        private void OnRollEnded()
        {
            _rollBehaviour.OnRollEnded -= OnRollEnded;

            _isCurrentRollCompleted = true;
        }
    }
}