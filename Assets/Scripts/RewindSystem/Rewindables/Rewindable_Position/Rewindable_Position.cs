using UnityEngine;

namespace BLUE.ACubeStory.RewindSystem
{
    public class Rewindable_Position : RewindableBase<RewindSnapShot_Position>
    {
        [SerializeField] private Transform _rootTransform = null;
        
        protected override RewindSnapShot_Position GetSnapshot()
        {
            return new RewindSnapShot_Position(_rootTransform.position);
        }

        protected override void RestoreSnapshot(RewindSnapShot_Position snapshot)
        {
            _rootTransform.position = snapshot.Position;
        }
    }
}