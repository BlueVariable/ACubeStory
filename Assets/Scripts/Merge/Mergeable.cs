using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    // TODO GOKAY: UNMERGE
    public class Mergeable : MonoBehaviour
    {
        [SerializeField] private ActionExecutor _mergedActionExecutor = null;
        
        [SerializeField] private Transform _mergeableObject = null;
        public Transform MergeableObject => _mergeableObject;

        public void Merged()
        {
            _mergedActionExecutor.Execute();
        }
        
        public Vector3 GetMergeablePos()
        {
            return _mergeableObject.position;
        }
    }
}