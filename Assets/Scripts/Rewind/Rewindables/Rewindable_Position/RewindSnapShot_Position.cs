using UnityEngine;

namespace BLUE.ACubeStory.Rewind
{
    public class RewindSnapShot_Position : RewindSnapshotBase
    {
        public Vector3 Position { get; }

        public RewindSnapShot_Position(Vector3 position)
        {
            Position = position;
        }

        public override bool IsIdentical(RewindSnapshotBase otherSnapshot)
        {
            if (!(otherSnapshot is RewindSnapShot_Position snapshot))
            {
                return false;
            }
            
            return Position == snapshot.Position;
        }
    }
}