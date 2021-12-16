using UnityEngine;

namespace BLUE.ACubeStory.Rewind
{
    public class Rewindable_Rotation : RewindableBase<RewindSnapShot_Rotation>
    {
        [SerializeField] private Transform _rootTransform = null;
        
        protected override RewindSnapShot_Rotation GetSnapshot()
        {
            return new RewindSnapShot_Rotation(_rootTransform.rotation);
        }

        protected override void RestoreSnapshot(RewindSnapShot_Rotation snapshot)
        {
            _rootTransform.rotation = snapshot.Rotation;
        }
    }
}