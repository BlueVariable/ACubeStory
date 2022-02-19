using System;
using UnityEngine;
using BLUE.Common;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Movement : RewindSnapshot_Cube
    {
        public Vector3 TargetPosition { get; }
        
        public MoveBehaviour_Tween.TweenMoveSettings MoveSettings { get; }
        
        private CubeStateRewinder_Movement _movementRewinder;

        public RewindSnapshot_Cube_Movement(CubeStateRewinder_Movement movementRewiner, Vector3 targetPosition, MoveBehaviour_Tween.TweenMoveSettings moveSettings)
        {
            _movementRewinder = movementRewiner;
            TargetPosition = targetPosition;
            MoveSettings = moveSettings;
        }

        public override void Rewind(Action completedCallback)
        {
            _movementRewinder.RewindState(this, () =>
            {
                completedCallback?.Invoke();
            });
        }
    }
}