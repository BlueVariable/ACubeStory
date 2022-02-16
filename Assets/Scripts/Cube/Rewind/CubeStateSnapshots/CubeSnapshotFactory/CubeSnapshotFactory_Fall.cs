using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeSnapshotFactory_Fall : CubeSnapshotFactory
    {
        [SerializeField] private Transform _cubeRootTransform = null;
        
        [SerializeField] private State_Cube_Fall _fallState = null;

        [SerializeField] private CubeStateRewinder_Fall _fallRewinder = null;
        
        private Action<RewindSnapshot_Cube> _createdCallback;

        public override ECubeState GetCubeState()
        {
            return ECubeState.Fall;
        }

        public override bool CreateSnapshot(Action<RewindSnapshot_Cube> createdCallback)
        {
            _createdCallback = createdCallback;
            
            RegisterToFallState();
            
            return false;
        }

        private void RegisterToFallState()
        {
            _fallState.OnFallStartedTowardsHitObject += OnFallStartedTowardsToObject;
            _fallState.OnFallStartedTowardsVoid += OnFallStartedTowardsVoid;
            _fallState.OnFallSkipped += OnFallSkipped;
        }

        private void UnregisterFromFallState()
        {
            _fallState.OnFallStartedTowardsHitObject -= OnFallStartedTowardsToObject;
            _fallState.OnFallStartedTowardsVoid -= OnFallStartedTowardsVoid;
            _fallState.OnFallSkipped -= OnFallSkipped;
        }

        private void OnFallStartedTowardsToObject(RaycastHit hit)
        {
            CreateSnapshot();

            UnregisterFromFallState();
        }

        private void OnFallStartedTowardsVoid()
        {
            CreateSnapshot();
            
            UnregisterFromFallState();
        }
        
        private void OnFallSkipped()
        {
            UnregisterFromFallState();
        }

        private RewindSnapshot_Cube_Fall CreateSnapshot()
        {
            Vector3 fallStartPos = _cubeRootTransform.position;

            RewindSnapshot_Cube_Fall snapshot = new RewindSnapshot_Cube_Fall(_fallRewinder, fallStartPos, _cubeRootTransform);

            _createdCallback?.Invoke(snapshot);
            
            return snapshot;
        }
    }
    
}