using System;
using BLUE.ACubeStory.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeSnapshotFactory_Climb : CubeSnapshotFactory_RollBased
    {
        [SerializeField] private RollBehaviourBase _rollBehaviour = null;
        
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

            RewindSnapshot_Cube_Roll rollSnapshot = 
                new RewindSnapshot_Cube_Roll(
                    _rollRewinder,
                    -_rollBehaviour.LastRollDirection, 
                    _rollSpeed, 
                    _rollAngle,
                    _rollCurve, 
                    _rollPointOffset);
            
            _createdCallback?.Invoke(rollSnapshot);
        }
    }
}