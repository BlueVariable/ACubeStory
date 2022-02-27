using UnityEngine;
using BLUE.ACubeStory.Merge;
using BLUE.ACubeStory.Cube.src;

namespace BLUE.ACubeStory.Cube
{
    public class Rewindable_Cube_MergeComposition : RewindableBase<RewindController_Cube, RewindSnapshot_Cube>
    {
        [SerializeField] private MergerBase _merger = null;

        [SerializeField] private MergeableComposition _mergeableComposition = null;
        
        private void Awake()
        {
            RegisterToMerger();
        }

        private void OnDestroy()
        {
            UnregisterFromMerger();
        }

        private void RegisterToMerger()
        {
            _merger.OnMerged += OnMerged;
        }

        private void UnregisterFromMerger()
        {
            _merger.OnMerged -= OnMerged;
        }

        private void OnMerged(Mergeable formerMergeable, Mergeable newMergeable)
        {
            SaveSnapshot(new RewindSnapshot_Merge(newMergeable, _merger, _mergeableComposition));
        }
    }
}