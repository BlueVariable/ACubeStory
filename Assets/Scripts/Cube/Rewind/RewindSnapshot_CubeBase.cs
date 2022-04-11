using BLUE.RewindSystem;

namespace BLUE.ACubeStory.Cube
{
    public abstract class RewindSnapshot_CubeBase : RewindSnapshotBase
    {
        public ECubeState CubeState { get; }

        protected RewindSnapshot_CubeBase(ECubeState cubeState = ECubeState.None)
        {
            CubeState = cubeState;
        }
    }
}