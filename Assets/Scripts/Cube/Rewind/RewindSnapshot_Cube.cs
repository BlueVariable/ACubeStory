using System;
using BLUE.ACubeStory.Cube.src;
using BLUE.ACubeStory.Cube.StateRewinder;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube : RewindSnapshotBase
    {
        private CubeStateRewinderBase _stateRewinder { get; }

        public RewindSnapshot_Cube(CubeStateRewinderBase stateRewinder)
        {
            _stateRewinder = stateRewinder;
        }

        public override void Undo(Action completedCallback)
        {
            _stateRewinder.RewindState(completedCallback);
        }
    }
}