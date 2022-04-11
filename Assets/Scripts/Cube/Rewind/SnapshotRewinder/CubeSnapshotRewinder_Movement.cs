using System;
using UnityEngine;
using BLUE.Common;

namespace BLUE.ACubeStory.Cube
{
    public class CubeSnapshotRewinder_Movement : CubeSnapshotRewinderBase<RewindSnapshot_Cube_Movement>
    {
        [SerializeField] private MoveBehaviour_Tween _moveBehaviour = null;

        public override void RewindSnapshot(RewindSnapshot_Cube_Movement snapshot, Action onCompleted)
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