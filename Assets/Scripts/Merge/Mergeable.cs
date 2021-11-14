using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class Mergeable : MonoBehaviour
    {
        [SerializeField] private Transform _mergeableObject = null;
        public Transform MergeableObject => _mergeableObject;
        
        public Vector3 GetMergeablePos()
        {
            return _mergeableObject.position;
        }
    }
}