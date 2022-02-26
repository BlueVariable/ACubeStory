using System;
using BLUE.ACubeStory.Merge;
using BLUE.ACubeStory.Cube.src;

namespace BLUE.ACubeStory.Cube.Snapshots
{
    public class RewindSnapshot_MergeComposition_Mergeable : RewindSnapshot_MergeComposition
    {
        private MergeableComposition _mergeableComposition;
        
        private MergerBase _merger;

        private Mergeable _mergeable;

        public RewindSnapshot_MergeComposition_Mergeable(MergeableComposition mergeableComposition, MergerBase merger, Mergeable mergeable)
        {
            _mergeableComposition = mergeableComposition;
            _merger = merger;
            _mergeable = mergeable;
        }

        public override void Rewind(Action<RewindSnapshotBase> completedCallback)
        {
            bool res = _merger.TryUnmerge(_mergeableComposition.Mergeables, _mergeable);

            if (res)
            {
                completedCallback?.Invoke(this);
            }
            else
            {
                throw new Exception($"Mergeable Snapshot could not be rewinded");
            }
        }
    }
}