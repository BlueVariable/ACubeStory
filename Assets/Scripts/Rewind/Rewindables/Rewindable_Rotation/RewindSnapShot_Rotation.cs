using UnityEngine;

namespace BLUE.ACubeStory.Rewind
{
    public class RewindSnapShot_Rotation : RewindSnapshotBase
    {
        public Quaternion Rotation { get; }

        public RewindSnapShot_Rotation(Quaternion rotation)
        {
            Rotation = rotation;
        }
        
        public override bool IsIdentical(RewindSnapshotBase otherSnapshot)
        {
            if (!(otherSnapshot is RewindSnapShot_Rotation snapshot))
            {
                return false;
            }

            return Rotation == snapshot.Rotation;
        }
    }
}