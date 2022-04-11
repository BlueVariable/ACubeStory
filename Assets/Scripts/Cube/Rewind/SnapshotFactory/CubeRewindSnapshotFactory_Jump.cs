using System;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeRewindSnapshotFactory_Jump : CubeRewindSnapshotFactoryBase
    {
        [SerializeField] private Transform _cubeRootTransform = null;
        
        [SerializeField] private CubeSnapshotRewinder_Movement _movementRewinder = null;
        
        [SerializeField] private MoveBehaviour_Tween _moveBehaviour = null;
        
        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _jumpRewindSettings = null;
        
        public override ECubeState GetCubeState()
        {
            return ECubeState.Jump;
        }

        public override bool CreateSnapshot(Action<RewindSnapshot_CubeBase> createdCallback)
        {
            RewindSnapshot_Cube_Movement snapshot = 
                new RewindSnapshot_Cube_Movement(
                    ECubeState.Jump,
                    _movementRewinder,
                    _moveBehaviour,
                    _cubeRootTransform.position,
                    _jumpRewindSettings);
            
            createdCallback?.Invoke(snapshot);
            
            return true;
        }
    }
}