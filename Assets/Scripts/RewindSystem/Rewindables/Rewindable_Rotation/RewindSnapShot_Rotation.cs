using UnityEngine;

namespace BLUE.ACubeStory.RewindSystem
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