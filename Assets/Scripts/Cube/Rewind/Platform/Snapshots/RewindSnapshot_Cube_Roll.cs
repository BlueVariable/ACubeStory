using System;
using BLUE.ACubeStory.Common;
using BLUE.RewindSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Roll : RewindSnapshot_Cube_PlatformBase
    {
        #region Constants

        private const float UNDO_SPEED_MULTIPLIER = 2f;

        #endregion
        
        public Vector3 RollDirection { get; }
        
        public float RollSpeed { get; }
        
        public float RollAngle { get; }
        
        public AnimationCurve RollCurve { get; }
        
        public Vector3 RollPointOffset { get; }
        
        private CubeSnapshotRewinder_Roll _rollRewinder;

        private RollBehaviourBase _rollBehaviour;

        private bool _isCurrentRollCompleted;

        #region Events

        public Action OnRewindStarted { get; set; }

        #endregion

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
            _rollBehaviour.OnRollEnded -= OnRollEnded;
            
            OnRewindStarted?.Invoke();
            
            if (_isCurrentRollCompleted)
            {
                _rollRewinder.RewindSnapshot(this, () => { completedCallback?.Invoke(this); });
            }
            else
            {
                _rollBehaviour.TryUndoRoll(
                    true,
                    () => { completedCallback?.Invoke(this); },
                    UNDO_SPEED_MULTIPLIER);
            }
        }

        private void OnRollEnded()
        {
            _rollBehaviour.OnRollEnded -= OnRollEnded;

            _isCurrentRollCompleted = true;
        }
    }
}