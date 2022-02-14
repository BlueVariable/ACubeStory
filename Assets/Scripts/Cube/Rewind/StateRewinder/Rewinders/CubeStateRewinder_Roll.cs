using System;
using BLUE.ACubeStory.Cube.StateRewinder;

namespace BLUE.ACubeStory.Cube
{
    public class CubeStateRewinder_Roll : CubeStateRewinderBase<RewindSnapshot_Cube_Roll>
    {
        public override ECubeState GetRewinderState()
        {
            return ECubeState.Roll;
        }

        public override void RewindState(RewindSnapshot_Cube_Roll snapshot, Action onCompleted)
        {
            throw new NotImplementedException();
        }
    }
}