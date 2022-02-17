using System;
using BLUE.ACubeStory.Common;
using BLUE.ACubeStory.Cube.StateRewinder;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeStateRewinder_Climb : CubeStateRewinderBase<RewindSnapshot_Cube_Climb>
    {
        [SerializeField] private float _rollSpeed = 3f;
        [SerializeField] private AnimationCurve _rollCurve = default;
        [SerializeField] private RollBehaviourBase _rollBehaviour = null;        
        public override ECubeState GetRewinderState()
        {
            return ECubeState.Climb;
        }

        public override void RewindState(RewindSnapshot_Cube_Climb snapshot, Action onCompleted)
        {
            _rollBehaviour.TryRollTo(
                snapshot.ClimbDirection,
                180f,
                _rollSpeed,
                rollCurve: _rollCurve,
                onRolledCallback: () =>
                {
                    onCompleted?.Invoke();
                });
        }
    }
}