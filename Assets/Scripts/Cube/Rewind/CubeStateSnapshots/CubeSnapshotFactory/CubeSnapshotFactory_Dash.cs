using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeSnapshotFactory_Dash : CubeSnapshotFactory
    {
        [SerializeField] private Transform _cubeRootTransform = null;

        [SerializeField] private CubeStateRewinder_Dash _dashRewinder = null;
        
        public override ECubeState GetCubeState()
        {
            return ECubeState.Dash;
        }

        public override bool CreateSnapshot(Action<RewindSnapshot_Cube> createdCallback)
        {
            createdCallback?.Invoke(new RewindSnapshot_Cube_Dash(_dashRewinder, _cubeRootTransform.position));
            
            return true;
        }
    }
}