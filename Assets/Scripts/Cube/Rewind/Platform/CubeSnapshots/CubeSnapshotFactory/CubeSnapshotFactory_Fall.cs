using System;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeSnapshotFactory_Fall : CubeSnapshotFactory
    {
        [SerializeField] private Transform _cubeRootTransform = null;
        
        [SerializeField] private State_Cube_Fall _fallState = null;

        [SerializeField] private CubeSnapshotRewinder_Movement _movementRewinder = null;
        
        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _fallRewindSettings = null; 

        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _fallRewindSettingsHalfway = null; 
        
        [SerializeField] private MoveBehaviour_Tween _moveBehaviour = null;

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

        private RewindSnapshot_Cube CreateSnapshot()
        {
            Vector3 fallStartPos = _cubeRootTransform.position;

            RewindSnapshot_Cube_Fall fallSnapshot = new RewindSnapshot_Cube_Fall(
                ECubeState.Fall,
                _movementRewinder,
                fallStartPos,
                _moveBehaviour,
                _fallRewindSettings,
                _fallRewindSettingsHalfway,
                _fallState);

            _createdCallback?.Invoke(fallSnapshot);
            
            return fallSnapshot;
        }
    }
    
}