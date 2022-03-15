using System;
using System.Diagnostics;
using BLUE.ACubeStory.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeSnapshotFactory_Roll : CubeSnapshotFactory_RollBased
    {
        [SerializeField] private RollBehaviourBase _rollBehaviour = null;

        [SerializeField] private RewindController_Cube _rewindController = null;
        
        [SerializeField] private State_Cube_Roll _cubeRollState = null;

        private RewindSnapshot_Cube_Roll _lastSnapshot = null;
        
        public override ECubeState GetCubeState()
        {
            return ECubeState.Roll;
        }

        public override bool CreateSnapshot(Action<RewindSnapshot_Cube> createdCallback)
        {
            RegisterToRollBehaviour();
            
            RewindSnapshot_Cube_Roll rollSnapshot =  
                new RewindSnapshot_Cube_Roll(
                    ECubeState.Roll,
                    _rollRewinder,
                    _rollBehaviourProvider.GetRollBehaviour(),
                    -_cubeRollState.LastCandidateTransitionMessage.Direction,
                    _rollSpeed, 
                    _rollAngle,
                    _rollCurve, 
                    _rollPointOffset);

            rollSnapshot.OnRewindStarted += onSnapshotRewind;

            void onSnapshotRewind()
            {
                rollSnapshot.OnRewindStarted -= onSnapshotRewind;
                
                UnregisterFromRollBehaviour();
            }

            _lastSnapshot = rollSnapshot;

            createdCallback?.Invoke(_lastSnapshot);
            
            return true;
        }

        private void RegisterToRollBehaviour()
        {
            _rollBehaviour.OnRollEnded += OnRollEnded;
            _rollBehaviour.OnRollUndoStarted += OnUndoStarted;
        }

        private void UnregisterFromRollBehaviour()
        {
            _rollBehaviour.OnRollEnded -= OnRollEnded;
            _rollBehaviour.OnRollUndoStarted -= OnUndoStarted;
        }

        private void OnRollEnded()
        {
            UnregisterFromRollBehaviour();
        }

        private void OnUndoStarted()
        {
            UnregisterFromRollBehaviour();

            _rewindController.TryRemoveSnapshot(_lastSnapshot);
        }
    }
}