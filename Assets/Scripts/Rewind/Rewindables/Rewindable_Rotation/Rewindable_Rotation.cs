using UnityEngine;

namespace BLUE.ACubeStory.Rewind
{
    public class Rewindable_Rotation : RewindableBase<RewindSnapshot_Rotation>
    {
        [SerializeField] private Transform _rootTransform = null;
        
        protected override RewindSnapshot_Rotation GetSnapshot()
        {
            return new RewindSnapshot_Rotation(_rootTransform.rotation);
        }

        protected override void RestoreSnapshot(RewindSnapshot_Rotation snapshot)
        {
            _rootTransform.rotation = snapshot.Rotation;
        }
    }
}