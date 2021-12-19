using UnityEngine;

namespace BLUE.ACubeStory.Rewind
{
    public class RewindSnapshot_Position : RewindSnapshotBase
    {
        public Vector3 Position { get; }

        public RewindSnapshot_Position(Vector3 position)
        {
            Position = position;
        }

        public override bool IsIdentical(RewindSnapshotBase otherSnapshot)
        {
            if (!(otherSnapshot is RewindSnapshot_Position snapshot))
            {
                return false;
            }
            
            return Position == snapshot.Position;
        }
    }
}