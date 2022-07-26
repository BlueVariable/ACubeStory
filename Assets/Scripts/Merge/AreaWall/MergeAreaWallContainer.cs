using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class MergeAreaWallContainer : MonoBehaviour
    {
        [SerializeField] private MergeAreaWall _referenceWall = null;
        public MergeAreaWall ReferenceWall => _referenceWall;
    }
}