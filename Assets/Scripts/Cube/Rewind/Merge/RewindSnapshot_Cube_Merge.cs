using System;
using BLUE.ACubeStory.Merge;
using BLUE.RewindSystem;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Cube_Merge : RewindSnapshot_CubeBase
    {
        private Mergeable _mergeable;
        private MergeableComposition _mergeableComposition;

        public RewindSnapshot_Cube_Merge(Mergeable mergeable, MergeableComposition mergeableComposition)
        {
            _mergeable = mergeable;
            _mergeableComposition = mergeableComposition;
        }
        
        public override void Rewind(Action<RewindSnapshotBase> completedCallback)
        {
            _mergeableComposition.TryUnmerge(_mergeable);

            completedCallback?.Invoke(this);
        }
    }
}