using System;
using BLUE.ACubeStory.Cube.StateRewinder;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeStateRewinder_Fall : CubeStateRewinderBase<RewindSnapshot_Cube_Fall>
    {
        [SerializeField] private MoveBehaviour_Tween _moveBehaviour = null;
        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _moveSettings = null;
        
        public override ECubeState GetRewinderState()
        {
            return ECubeState.Fall;
        }

        public override void RewindState(RewindSnapshot_Cube_Fall snapshot, Action onCompleted)
        {
            _moveBehaviour.StartMovementTowards(
                _moveSettings,
                snapshot.FallStartPos,
                () =>
                    {
                        onCompleted?.Invoke();
                    });
        }
    }
}