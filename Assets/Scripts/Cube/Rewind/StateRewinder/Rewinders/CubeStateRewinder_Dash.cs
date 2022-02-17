using System;
using BLUE.ACubeStory.Cube.StateRewinder;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeStateRewinder_Dash : CubeStateRewinderBase<RewindSnapshot_Cube_Dash>
    {
        [SerializeField] private MoveBehaviour_Tween _moveBehaviour = null;
        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _moveSettings = null;

        public override ECubeState GetRewinderState()
        {
            return ECubeState.Dash;
        }

        public override void RewindState(RewindSnapshot_Cube_Dash snapshot, Action onCompleted)
        {
            _moveBehaviour.StartMovementTowards(
                _moveSettings,
                snapshot.RewindStartPos,
                () =>
                {
                    onCompleted?.Invoke();
                });
        }
    }
}