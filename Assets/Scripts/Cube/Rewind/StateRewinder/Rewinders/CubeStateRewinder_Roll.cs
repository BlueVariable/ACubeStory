using System;
using UnityEngine;
using BLUE.ACubeStory.Common;
using BLUE.ACubeStory.Cube.StateRewinder;

namespace BLUE.ACubeStory.Cube
{
    public class CubeStateRewinder_Roll : CubeStateRewinderBase<RewindSnapshot_Cube_Roll>
    {
        [SerializeField] private RollBehaviourProviderBase _rollBehaviourProvider = null;

        public override ECubeState GetRewinderState()
        {
            return ECubeState.Roll;
        }

        public override void RewindState(RewindSnapshot_Cube_Roll snapshot, Action onCompleted)
        {
            _rollBehaviourProvider.GetRollBehaviour().TryRollTo(
                -snapshot.RollDirection, 
                onRolledCallback: () =>
                {
                    onCompleted?.Invoke();
                });
        }
    }
}