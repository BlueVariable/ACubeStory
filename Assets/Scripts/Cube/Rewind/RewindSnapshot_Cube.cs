using BLUE.ACubeStory.Cube.src;

namespace BLUE.ACubeStory.Cube
{
    public abstract class RewindSnapshot_Cube : RewindSnapshotBase
    {
        public ECubeState CubeState { get; }

        protected RewindSnapshot_Cube(ECubeState cubeState = ECubeState.None)
        {
            CubeState = cubeState;
        }
    }
}