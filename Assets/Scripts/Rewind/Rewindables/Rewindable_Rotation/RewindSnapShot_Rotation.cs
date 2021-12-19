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
            if (otherSnapshot == null || !(otherSnapshot is RewindSnapShot_Rotation))
            {
                return false;
            }

            return Rotation == ((RewindSnapShot_Rotation) otherSnapshot).Rotation;
        }
    }
}