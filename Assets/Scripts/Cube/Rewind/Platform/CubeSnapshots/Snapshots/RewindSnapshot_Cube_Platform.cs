namespace BLUE.ACubeStory.Cube
{
    public abstract class RewindSnapshot_Cube_Platform : RewindSnapshot_Cube
    {
        public ECubeState CubeState { get; }

        protected RewindSnapshot_Cube_Platform(ECubeState cubeState)
        {
            CubeState = cubeState;
        }
    }
}