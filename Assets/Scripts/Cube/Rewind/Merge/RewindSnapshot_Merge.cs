using System;
using BLUE.ACubeStory.Merge;
using BLUE.ACubeStory.Cube.src;

namespace BLUE.ACubeStory.Cube
{
    public class RewindSnapshot_Merge : RewindSnapshot_Cube
    {
        private Mergeable _mergeable;
        private MergeableComposition _mergeableComposition;

        public RewindSnapshot_Merge(Mergeable mergeable, MergeableComposition mergeableComposition)
        {
            _mergeable = mergeable;
            _mergeableComposition = mergeableComposition;
        }

        public override void Rewind(Action<RewindSnapshotBase> completedCallback)
        {
            bool res = _mergeableComposition.TryUnmerge(_mergeable);

            if (!res)
            {
                // throw new Exception($"Rewind Merge Unmerge operation failed!..");
                
                UnityEngine.Debug.LogError($"Rewind Merge Unmerge operation failed!..");
            }
            
            completedCallback?.Invoke(this);
        }
    }
}