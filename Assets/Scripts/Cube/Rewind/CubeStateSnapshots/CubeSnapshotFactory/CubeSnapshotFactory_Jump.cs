using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeSnapshotFactory_Jump : CubeSnapshotFactory
    {
        [SerializeField] private Transform _cubeRootTransform = null;
        
        [SerializeField] private CubeStateRewinder_Jump _jumpRewinder = null;
        
        public override ECubeState GetCubeState()
        {
            return ECubeState.Jump;
        }

        public override bool CreateSnapshot(Action<RewindSnapshot_Cube> createdCallback)
        {
            RewindSnapshot_Cube_Jump snapshot = new RewindSnapshot_Cube_Jump(_jumpRewinder, _cubeRootTransform.position);
            
            createdCallback?.Invoke(snapshot);
            
            return true;
        }
    }
}