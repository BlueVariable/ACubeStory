using UnityEngine;
using BLUE.ACubeStory.Merge;
using BLUE.RewindSystem;

namespace BLUE.ACubeStory.Cube
{
    public class Rewindable_Cube_Merge : RewindableBase<RewindController_Cube, RewindSnapshot_CubeBase>
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
            if (_merger != null)
            {
                _merger.OnMerged -= OnMerged;
            }
        }

        private void OnMerged(Mergeable formerMergeable, Mergeable newMergeable)
        {
            if (!_mergeableComposition.Mergeables.Contains(newMergeable))
            {
                SaveSnapshot(new RewindSnapshot_Cube_Merge(newMergeable, _mergeableComposition));  
            };
        }
    }
}