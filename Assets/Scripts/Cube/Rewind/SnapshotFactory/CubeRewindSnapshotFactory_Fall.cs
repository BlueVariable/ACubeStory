using System;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeRewindSnapshotFactory_Fall : CubeRewindSnapshotFactoryBase
    {
        [SerializeField] private Transform _cubeRootTransform = null;
        
        [SerializeField] private State_Cube_Fall _fallState = null;

        [SerializeField] private CubeSnapshotRewinder_Movement _movementRewinder = null;
        
        [SerializeField] private MoveBehaviour_Tween _moveBehaviour = null;
        
        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _fallRewindSettingsFromObject = null; 

        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _fallRewindSettingsFromVoid = null;

        private Action<RewindSnapshot_CubeBase> _createdCallback;

        public override ECubeState GetCubeState()
        {
            return ECubeState.Fall;
        }

        public override bool CreateSnapshot(Action<RewindSnapshot_CubeBase> createdCallback)
        {
            _createdCallback = createdCallback;
            
            RegisterToFallState();
            
            return false;
        }

        private void RegisterToFallState()
        {
            _fallState.OnFallStartedTowardsHitObject += OnFallStartedTowardsObject;
            _fallState.OnFallStartedTowardsVoid += OnFallStartedTowardsVoid;
            _fallState.OnFallSkipped += OnFallSkipped;
        }

        private void UnregisterFromFallState()
        {
            _fallState.OnFallStartedTowardsHitObject -= OnFallStartedTowardsObject;
            _fallState.OnFallStartedTowardsVoid -= OnFallStartedTowardsVoid;
            _fallState.OnFallSkipped -= OnFallSkipped;
        }

        private void OnFallStartedTowardsObject(RaycastHit hit)
        {
            CreateSnapshot(_fallRewindSettingsFromObject);

            UnregisterFromFallState();
        }

        private void OnFallStartedTowardsVoid()
        {
            CreateSnapshot(_fallRewindSettingsFromVoid);
            
            UnregisterFromFallState();
        }
        
        private void OnFallSkipped()
        {
            UnregisterFromFallState();
        }

        private RewindSnapshot_CubeBase CreateSnapshot(MoveBehaviour_Tween.TweenMoveSettings moveSettings)
        {
            Vector3 fallStartPos = _cubeRootTransform.position;

            RewindSnapshot_Cube_Movement fallSnapshot = new RewindSnapshot_Cube_Movement(
                ECubeState.Fall,
                _movementRewinder,
                _moveBehaviour,
                fallStartPos,
                moveSettings);

            _createdCallback?.Invoke(fallSnapshot);
            
            return fallSnapshot;
        }
    }
}