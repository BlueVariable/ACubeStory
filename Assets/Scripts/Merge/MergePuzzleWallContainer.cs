using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class MergePuzzleWallContainer : MonoBehaviour
    {
        [SerializeField] private MergeAreaWall _referenceWall = null;
        public MergeAreaWall ReferenceWall => _referenceWall;
    }
}