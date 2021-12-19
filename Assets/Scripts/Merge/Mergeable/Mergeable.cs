using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class Mergeable : MonoBehaviour
    {
        [SerializeField] private ActionExecutor _mergedActionExecutor = null;
        
        [SerializeField] private ActionExecutor _unmergedActionExecutor = null;

        public bool IsMerged { get; private set; } = false;

        public void Merged()
        {
            IsMerged = true;
            
            _mergedActionExecutor.Execute();
        }
        
        public void Unmerged()
        {
            IsMerged = false;
            
            _unmergedActionExecutor.Execute();
        }
        
        public Vector3 GetMergeablePos()
        {
            return transform.position;
        }
    }
}