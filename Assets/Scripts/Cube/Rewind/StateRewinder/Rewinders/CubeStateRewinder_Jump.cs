using System;
using BLUE.ACubeStory.Cube.StateRewinder;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeStateRewinder_Jump : CubeStateRewinderBase<RewindSnapshot_Cube_Jump>
    {
        [SerializeField] private MoveBehaviour_Tween _moveBehaviour = null;
        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _moveSettings = null;
        
        public override ECubeState GetRewinderState()
        {
            return ECubeState.Jump;
        }

        public override void RewindState(RewindSnapshot_Cube_Jump snapshot, Action onCompleted)
        {
            _moveBehaviour.StartMovementTowards(
                _moveSettings,
                snapshot.JumpStartPosition,
                () =>
                {
                    onCompleted?.Invoke();
                });
        }
    }
}