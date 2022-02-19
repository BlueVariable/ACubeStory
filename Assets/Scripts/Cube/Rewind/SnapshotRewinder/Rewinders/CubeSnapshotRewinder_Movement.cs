using System;
using UnityEngine;
using BLUE.Common;
using BLUE.ACubeStory.Cube.StateRewinder;

namespace BLUE.ACubeStory.Cube
{
    public class CubeSnapshotRewinder_Movement : CubeSnapshotRewinder<RewindSnapshot_Cube_Movement>
    {
        [SerializeField] private MoveBehaviour_Tween _moveBehaviour = null;

        public override void RewindState(RewindSnapshot_Cube_Movement snapshot, Action onCompleted)
        {
            _moveBehaviour.StartMovementTowards(
                snapshot.MoveSettings,
                snapshot.TargetPosition,
                () =>
                {
                    onCompleted?.Invoke();
                });
        }
    }
}