using System;

namespace BLUE.ACubeStory.Cube.src
{
    public abstract class RewindSnapshotBase
    {
        public abstract void Rewind(Action completedCallback);
    }
}