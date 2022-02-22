using System;

namespace BLUE.ACubeStory.Cube
{
    public class CubeSnapshotFactory_Descend : CubeSnapshotFactory_RollBased
    {
        private Action<RewindSnapshot_Cube> _createdCallback;
        
        public override ECubeState GetCubeState()
        {
            return ECubeState.Descend;
        }

        public override bool CreateSnapshot(Action<RewindSnapshot_Cube> createdCallback)
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
                    _rollRewinder,
                    -_rollBehaviourProvider.GetRollBehaviour().LastRollDirection, 
                    _rollSpeed, 
                    _rollAngle,
                    _rollCurve, 
                    _rollPointOffset);
            
            _createdCallback?.Invoke(rollSnapshot);
        }
    }
}