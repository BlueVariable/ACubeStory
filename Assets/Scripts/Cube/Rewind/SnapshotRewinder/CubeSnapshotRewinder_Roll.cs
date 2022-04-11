using System;
using UnityEngine;
using BLUE.ACubeStory.Common;

namespace BLUE.ACubeStory.Cube
{
    public class CubeSnapshotRewinder_Roll : CubeSnapshotRewinderBase<RewindSnapshot_Cube_Roll>
    {
        [SerializeField] private RollBehaviourProviderBase _rollBehaviourProvider = null;
        
        public override void RewindSnapshot(RewindSnapshot_Cube_Roll snapshot, Action onCompleted)
        {
            _rollBehaviourProvider
                .GetRollBehaviour()
                    .TryRollTo(
                    snapshot.RollDirection,
                    snapshot.RollAngle,
                    snapshot.RollSpeed,
                    snapshot.RollPointOffset,
                    snapshot.RollCurve,
                    () =>
                    {
                        onCompleted?.Invoke();
                    });
        }
    }
}