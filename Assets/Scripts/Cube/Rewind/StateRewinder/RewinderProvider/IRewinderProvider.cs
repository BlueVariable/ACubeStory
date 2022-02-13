using BLUE.ACubeStory.Cube.StateRewinder;

namespace BLUE.ACubeStory.Cube
{
    public interface IRewinderProvider
    {
        CubeStateRewinderBase GetRewinderBase(ECubeState state);
    }
}