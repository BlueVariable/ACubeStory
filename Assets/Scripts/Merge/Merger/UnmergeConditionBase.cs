using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public abstract class UnmergeConditionBase : MonoBehaviour
    {
        public abstract bool CanUnmerge(Mergeable mergeable);
    }
}