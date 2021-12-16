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
    }
}