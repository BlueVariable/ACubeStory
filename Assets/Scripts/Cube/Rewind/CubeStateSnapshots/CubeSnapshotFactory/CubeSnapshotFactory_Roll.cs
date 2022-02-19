using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeSnapshotFactory_Roll : CubeSnapshotFactory_RollBased
    {
        [SerializeField] private State_Cube_Roll _cubeRollState = null;
        
        public override ECubeState GetCubeState()
        {
            return ECubeState.Roll;
        }

        public override bool CreateSnapshot(Action<RewindSnapshot_Cube> createdCallback)
        {
            RewindSnapshot_Cube_Roll rollSnapshot = 
                new RewindSnapshot_Cube_Roll(
                    _rollRewinder,
                    -_cubeRollState.LastCandidateTransitionMessage.Direction,
                    _rollSpeed, 
                    _rollAngle,
                    _rollCurve, 
                    _rollPointOffset);
            
            createdCallback?.Invoke(rollSnapshot);
            
            return true;
        }
    }
}