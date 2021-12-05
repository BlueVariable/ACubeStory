using UnityEngine;

namespace BLUE.ACubeStory.RewindSystem
{
    public class RewindSnapShot_Position : RewindSnapshotBase
    {
        public Vector3 Position { get; }

        public RewindSnapShot_Position(Vector3 position)
        {
            Position = position;
        }
    }
}