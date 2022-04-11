using System;

namespace BLUE.ACubeStory.Cube
{
    public class CubeRewindSnapshotFactory_Roll_Climb : CubeRewindSnapshotFactory_RollBase
    {
        private Action<RewindSnapshot_CubeBase> _createdCallback;

        public override ECubeState GetCubeState()
        {
            return ECubeState.Climb;
        }

        public override bool CreateSnapshot(Action<RewindSnapshot_CubeBase> createdCallback)
        {
            _createdCallback = createdCallback;

            RegisterToRollBehaviour();

            return false;
        }

        private void RegisterToRollBehaviour()
        {
            _rollBehaviourProvider.GetRollBehaviour().OnRollStarted += OnClimbStarted;
        }

        private void UnregisterFromRollBehaviour()
        {
            _rollBehaviourProvider.GetRollBehaviour().OnRollStarted -= OnClimbStarted;
        }

        private void OnClimbStarted()
        {
            UnregisterFromRollBehaviour();

            RewindSnapshot_Cube_Roll rollSnapshot = 
                new RewindSnapshot_Cube_Roll(
                    ECubeState.Climb,
                    _rollRewinder,
                    _rollBehaviourProvider.GetRollBehaviour(),
                    -_rollBehaviourProvider.GetRollBehaviour().LastRollDirection, 
                    _rollSpeed, 
                    _rollAngle,
                    _rollCurve, 
                    _rollPointOffset);
            
            _createdCallback?.Invoke(rollSnapshot);
        }
    }
}