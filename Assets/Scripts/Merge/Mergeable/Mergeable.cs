using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    // TODO GOKAY: UNMERGE
    public class Mergeable : MonoBehaviour
    {
        [SerializeField] private ActionExecutor _mergedActionExecutor = null;

        public void Merged()
        {
            _mergedActionExecutor.Execute();
        }
        
        public Vector3 GetMergeablePos()
        {
            return transform.position;
        }
    }
}