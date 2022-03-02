using System;
using BLUE.ACubeStory.Cube.src;
using UnityEngine;
using BLUE.Common;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Movement : RewindSnapshot_Cube_Platform
    {
        public Vector3 TargetPosition { get; }
        
        public MoveBehaviour_Tween.TweenMoveSettings MoveSettings { get; protected set; }
        
        private CubeSnapshotRewinder_Movement _movementRewinder;
        private MoveBehaviour_Tween _moveBehaviour;
        private float _rewindSpeed = 1;

        private bool _curMovementCompleted;
        
        public RewindSnapshot_Cube_Movement(
            ECubeState cubeState,
            CubeSnapshotRewinder_Movement movementRewinder, 
            MoveBehaviour_Tween moveBehaviour,
            Vector3 targetPosition, 
            MoveBehaviour_Tween.TweenMoveSettings moveSettings,
            float rewindSpeed = 2)
        : base(cubeState)
        {
            _movementRewinder = movementRewinder;
            _moveBehaviour = moveBehaviour;
            _rewindSpeed = rewindSpeed;
            TargetPosition = targetPosition;
            MoveSettings = moveSettings;

            _moveBehaviour.OnMovementCompleted += OnMovementCompleted;
        }

        public override void Rewind(Action<RewindSnapshotBase> completedCallback)
        {
            if (_curMovementCompleted)
            {
                _movementRewinder.RewindState(this, () => { completedCallback?.Invoke(this); });
            }
            else
            {
                _moveBehaviour.TryInterruptAndSmoothRewind(
                    _rewindSpeed,
                    () => { completedCallback?.Invoke(this); });
            }
        }

        private void OnMovementCompleted()
        {
            _moveBehaviour.OnMovementCompleted -= OnMovementCompleted;
            
            _curMovementCompleted = true;
        }
    }
}