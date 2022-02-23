using System;
using BLUE.ACubeStory.Cube.src;
using UnityEngine;
using BLUE.Common;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Movement : RewindSnapshot_Cube
    {
        public Vector3 TargetPosition { get; }
        
        public MoveBehaviour_Tween.TweenMoveSettings MoveSettings { get; protected set; }
        
        private CubeSnapshotRewinder_Movement _movementRewinder;

        public RewindSnapshot_Cube_Movement(
            ECubeState cubeState,
            CubeSnapshotRewinder_Movement movementRewinder, 
            Vector3 targetPosition, 
            MoveBehaviour_Tween.TweenMoveSettings moveSettings) 
        : base(cubeState)
        {
            _movementRewinder = movementRewinder;
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