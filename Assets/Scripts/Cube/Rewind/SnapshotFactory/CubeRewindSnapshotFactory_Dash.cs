using System;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeRewindSnapshotFactory_Dash : CubeRewindSnapshotFactoryBase
    {
        [SerializeField] private Transform _cubeRootTransform = null;

        [SerializeField] private CubeSnapshotRewinder_Movement _movementRewinder = null;

        [SerializeField] private MoveBehaviour_Tween _moveBehaviour = null;
        
        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _dashRewindSettings = null;
        
        public override ECubeState GetCubeState()
        {
            return ECubeState.Dash;
        }

        public override bool CreateSnapshot(Action<RewindSnapshot_CubeBase> createdCallback)
        {
            createdCallback?.Invoke(
                new RewindSnapshot_Cube_Movement(
                    ECubeState.Dash,
                    _movementRewinder,
                    _moveBehaviour,
                    _cubeRootTransform.position, 
                    _dashRewindSettings));
            
            return true;
        }
    }
}