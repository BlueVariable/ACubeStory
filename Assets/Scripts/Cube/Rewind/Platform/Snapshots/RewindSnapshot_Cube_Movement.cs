using System;
using UnityEngine;
using BLUE.Common;
using BLUE.RewindSystem;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Movement : RewindSnapshot_Cube_PlatformBase
    {
        #region Constants

        private const float FALLBACK_REWIND_SPEED_MULTIPLIER = 2f;

        #endregion
        
        public Vector3 TargetPosition { get; }
        
        public MoveBehaviour_Tween.TweenMoveSettings MoveSettings { get; }
        
        private CubeSnapshotRewinder_Movement _movementRewinder;
        
        private MoveBehaviour_Tween _moveBehaviour;

        private bool _curMovementCompleted;
        
        public RewindSnapshot_Cube_Movement(
            ECubeState cubeState,
            CubeSnapshotRewinder_Movement movementRewinder, 
            MoveBehaviour_Tween moveBehaviour,
            Vector3 targetPosition, 
            MoveBehaviour_Tween.TweenMoveSettings moveSettings)
        : base(cubeState)
        {
            _movementRewinder = movementRewinder;
            _moveBehaviour = moveBehaviour;
            TargetPosition = targetPosition;
            MoveSettings = moveSettings;
            
            _moveBehaviour.OnMovementCompleted += OnMovementCompleted;
        }

        public override void Rewind(Action<RewindSnapshotBase> completedCallback)
        {
            _moveBehaviour.OnMovementCompleted -= OnMovementCompleted;

            if (!_curMovementCompleted)
            {
                if (_moveBehaviour.TryInterruptAndSmoothRewind(
                    FALLBACK_REWIND_SPEED_MULTIPLIER, () => { completedCallback?.Invoke(this); }))
                {
                    return;
                }
            }
            
            _movementRewinder.RewindSnapshot(this, () => { completedCallback?.Invoke(this); });
        }

        private void OnMovementCompleted()
        {
            _moveBehaviour.OnMovementCompleted -= OnMovementCompleted;
            
            _curMovementCompleted = true;
        }
    }
}