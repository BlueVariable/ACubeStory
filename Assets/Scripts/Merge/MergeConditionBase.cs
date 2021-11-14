using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public abstract class MergeConditionBase : MonoBehaviour
    {
        public abstract bool CanMerge(Mergeable formerMergeable, Mergeable candidateMergeable);
    }
}