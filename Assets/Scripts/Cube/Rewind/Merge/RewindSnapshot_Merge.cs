using System;
using BLUE.ACubeStory.Merge;
using BLUE.ACubeStory.Cube.src;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Merge : RewindSnapshot_Cube
    {
        private Mergeable _mergeable;
        private MergerBase _merger;
        private MergeableComposition _mergeableComposition;

        public RewindSnapshot_Merge(Mergeable mergeable, MergerBase merger, MergeableComposition mergeableComposition)
        {
            _mergeable = mergeable;
            _merger = merger;
            _mergeableComposition = mergeableComposition;
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
                throw new Exception($"Rewind Merge Unmerge operation failed!..");
            }
        }
    }
}