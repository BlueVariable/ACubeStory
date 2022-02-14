using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeSnapshotFactory_Roll : CubeSnapshotFactory
    {
        [SerializeField] private State_Cube_Roll _cubeRollState = null;
        
        [SerializeField] private CubeStateRewinder_Roll _rollRewinder = null;
        
        public override ECubeState GetCubeState()
        {
            return ECubeState.Roll;
        }

        public override RewindSnapshot_Cube CreateSnapshot()
        {
            return new RewindSnapshot_Cube_Roll(_rollRewinder, _cubeRollState.LastCandidateTransitionMessage.Direction);
        }
    }
}