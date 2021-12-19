using UnityEngine;

namespace BLUE.ACubeStory.Rewind
{
    public class Rewindable_Position : RewindableBase<RewindSnapshot_Position>
    {
        [SerializeField] private Transform _rootTransform = null;
        
        protected override RewindSnapshot_Position GetSnapshot()
        {
            return new RewindSnapshot_Position(_rootTransform.position);
        }

        protected override void RestoreSnapshot(RewindSnapshot_Position snapshot)
        {
            _rootTransform.position = snapshot.Position;
        }
    }
}