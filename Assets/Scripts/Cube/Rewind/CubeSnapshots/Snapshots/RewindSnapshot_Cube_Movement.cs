using System;
using BLUE.ACubeStory.Cube.src;
using UnityEngine;
using BLUE.Common;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Movement : RewindSnapshot_Cube
    {
        public Vector3 TargetPosition { get; }
        
        public MoveBehaviour_Tween.TweenMoveSettings MoveSettings { get; }
        
        private CubeSnapshotRewinder_Movement _movementRewinder;

        public RewindSnapshot_Cube_Movement(
            ECubeState cubeState,
            CubeSnapshotRewinder_Movement movementRewiner, 
            Vector3 targetPosition, 
            MoveBehaviour_Tween.TweenMoveSettings moveSettings) 
        : base(cubeState)
        {
            _movementRewinder = movementRewiner;
            TargetPosition = targetPosition;
            MoveSettings = moveSettings;
        }

        public override void Rewind(Action<RewindSnapshotBase> completedCallback)
        {
            _movementRewinder.RewindState(this, () =>
            {
                completedCallback?.Invoke(this);
            });
        }
    }
}