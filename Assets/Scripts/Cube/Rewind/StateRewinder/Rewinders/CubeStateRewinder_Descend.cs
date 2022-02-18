using System;
using BLUE.ACubeStory.Common;
using BLUE.ACubeStory.Cube.StateRewinder;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeStateRewinder_Descend : CubeStateRewinderBase<RewindSnapshot_Cube_Descend>
    {
        [SerializeField] private float _rollSpeed = 3f;
        [SerializeField] private AnimationCurve _rollCurve = null;
        [SerializeField] private RollBehaviourProviderBase _rollBehaviourProvider = null;
        
        public override ECubeState GetRewinderState()
        {
            return ECubeState.Descend;
        }

        public override void RewindState(RewindSnapshot_Cube_Descend snapshot, Action onCompleted)
        {
            _rollBehaviourProvider.GetRollBehaviour().TryRollTo(
                -snapshot.DescendDirection,
                180f,
                _rollSpeed,
                new Vector3(0, 1, 0),
                _rollCurve,
                onCompleted);
        }
    }
}