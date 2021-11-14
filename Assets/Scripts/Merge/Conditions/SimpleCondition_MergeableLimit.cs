using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class SimpleCondition_MergeableLimit : SimpleConditionBase
    {
        [SerializeField] private MergeableComposition _mergeableComposition = null;

        [SerializeField] private int _maxMergeableCount = 1;

        public override bool IsConditionSatisfied()
        {
            return _mergeableComposition.Mergeables.Count <= _maxMergeableCount;
        }
    }
}