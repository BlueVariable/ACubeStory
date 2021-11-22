using BLUE.ACubeStory.Merge;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class StateEnterCondition_MergeableLimit : StateEnterConditionBase
    {
        [SerializeField] private MergeableComposition _mergeableComposition = null;

        [SerializeField] private int _maxMergeableCount = 1;
        
        public override bool CanStateBeEntered()
        {
            return _maxMergeableCount >= _mergeableComposition.Mergeables.Count;
        }
    }
}