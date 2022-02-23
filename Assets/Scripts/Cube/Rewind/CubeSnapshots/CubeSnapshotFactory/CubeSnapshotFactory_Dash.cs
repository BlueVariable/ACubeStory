using System;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeSnapshotFactory_Dash : CubeSnapshotFactory
    {
        [SerializeField] private Transform _cubeRootTransform = null;

        [SerializeField] private CubeSnapshotRewinder_Movement _movementRewinder = null;

        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _dashRewindSettings = null; 

        public override ECubeState GetCubeState()
        {
            return ECubeState.Dash;
        }

        public override bool CreateSnapshot(Action<RewindSnapshot_Cube> createdCallback)
        {
            createdCallback?.Invoke(
                new RewindSnapshot_Cube_Movement(
                    ECubeState.Dash,
                    _movementRewinder,
                    _cubeRootTransform.position, 
                    _dashRewindSettings));
            
            return true;
        }
    }
}