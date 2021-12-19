using UnityEngine;

namespace BLUE.ACubeStory.Rewind
{
    public class RewindSnapshot_Rotation : RewindSnapshotBase
    {
        public Quaternion Rotation { get; }

        public RewindSnapshot_Rotation(Quaternion rotation)
        {
            Rotation = rotation;
        }
        
        public override bool IsIdentical(RewindSnapshotBase otherSnapshot)
        {
            if (!(otherSnapshot is RewindSnapshot_Rotation snapshot))
            {
                return false;
            }

            return Rotation == snapshot.Rotation;
        }
    }
}