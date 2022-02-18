using System;
using BLUE.ACubeStory.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeSnapshotFactory_Climb : CubeSnapshotFactory
    {
        [SerializeField] private RollBehaviourBase _rollBehaviour = null;

        [SerializeField] private CubeStateRewinder_Climb _climbRewinder = null;
        
        private Action<RewindSnapshot_Cube> _createdCallback;

        public override ECubeState GetCubeState()
        {
            return ECubeState.Climb;
        }

        public override bool CreateSnapshot(Action<RewindSnapshot_Cube> createdCallback)
        {
            _createdCallback = createdCallback;

            RegisterToRollBehaviour();

            return false;
        }

        private void RegisterToRollBehaviour()
        {
            _rollBehaviour.OnRollStarted += OnClimbStarted;
        }

        private void UnregisterFromRollBehaviour()
        {
            _rollBehaviour.OnRollStarted -= OnClimbStarted;
        }

        private void OnClimbStarted()
        {
            UnregisterFromRollBehaviour();
            
            _createdCallback?.Invoke(new RewindSnapshot_Cube_Climb(_climbRewinder, _rollBehaviour.LastRollDirection));
        }
    }
}