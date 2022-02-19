using System;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeSnapshotFactory_Jump : CubeSnapshotFactory
    {
        [SerializeField] private Transform _cubeRootTransform = null;
        
        [SerializeField] private CubeSnapshotRewinder_Movement _movementRewinder = null;
        
        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _jumpRewindSettings = null;

        public override ECubeState GetCubeState()
        {
            return ECubeState.Jump;
        }

        public override bool CreateSnapshot(Action<RewindSnapshot_Cube> createdCallback)
        {
            RewindSnapshot_Cube_Movement snapshot = 
                new RewindSnapshot_Cube_Movement(
                    _movementRewinder,
                    _cubeRootTransform.position,
                    _jumpRewindSettings);
            
            createdCallback?.Invoke(snapshot);
            
            return true;
        }
    }
}