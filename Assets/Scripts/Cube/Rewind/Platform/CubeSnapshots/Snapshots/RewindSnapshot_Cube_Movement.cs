using System;
using BLUE.ACubeStory.Cube.src;
using UnityEngine;
using BLUE.Common;
using BLUE.Helpers;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Movement : RewindSnapshot_Cube_Platform
    {
        public Vector3 TargetPosition { get; }
        
        public MoveBehaviour_Tween.TweenMoveSettings MoveSettings { get; }
        
        private CubeSnapshotRewinder_Movement _movementRewinder;
        private MoveBehaviour_Tween _moveBehaviour;
        private float _rewindSpeed;
        private bool _forceTweenBack = false;
        
        private bool _curMovementCompleted;
        
        public RewindSnapshot_Cube_Movement(
            ECubeState cubeState,
            CubeSnapshotRewinder_Movement movementRewinder, 
            MoveBehaviour_Tween moveBehaviour,
            Vector3 targetPosition, 
            MoveBehaviour_Tween.TweenMoveSettings moveSettings,
            float rewindSpeed = 4,
            bool forceTweenBack = false)
        : base(cubeState)
        {
            _movementRewinder = movementRewinder;
            _moveBehaviour = moveBehaviour;
            _rewindSpeed = rewindSpeed;
            _forceTweenBack = forceTweenBack;
            TargetPosition = targetPosition;
            MoveSettings = moveSettings;

            if (!forceTweenBack)
            {
                CoroutineHelper.Instance.WaitForFixedUpdate(() =>
                {
                    _moveBehaviour.OnMovementCompleted += OnMovementCompleted;
                });
            }
        }

        public override void Rewind(Action<RewindSnapshotBase> completedCallback)
        {
            _moveBehaviour.OnMovementCompleted -= OnMovementCompleted;

            if (_forceTweenBack)
            {
                _movementRewinder.RewindState(this, () => { completedCallback?.Invoke(this); });
                
                return;
            }

            if (!_curMovementCompleted)
            {
                if (_moveBehaviour.TryInterruptAndSmoothRewind(
                        _rewindSpeed, () => { completedCallback?.Invoke(this); }))
                {
                    return;
                }
            }
            
            _movementRewinder.RewindState(this, () => { completedCallback?.Invoke(this); });
        }

        private void OnMovementCompleted()
        {
            _moveBehaviour.OnMovementCompleted -= OnMovementCompleted;
            
            _curMovementCompleted = true;
        }
    }
}