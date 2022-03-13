using BLUE.ACubeStory.Cube.src;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class RewindController_Cube : RewindControllerBase<RewindSnapshot_Cube>
    {
        protected override void SaveSnapshotCustomActions(RewindSnapshot_Cube snapshot)
        {
            // UnityEngine.Debug.Log($"Snapshot saved : {snapshot.CubeState} - {Time.frameCount}");
            
            base.SaveSnapshotCustomActions(snapshot);
        }

        protected override void TryRewindCustomActions()
        {
            base.TryRewindCustomActions();
        }
    }
}