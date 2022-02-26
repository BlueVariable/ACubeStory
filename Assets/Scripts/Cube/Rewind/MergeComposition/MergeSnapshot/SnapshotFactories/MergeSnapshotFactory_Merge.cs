using UnityEngine;
using BLUE.ACubeStory.Merge;
using BLUE.ACubeStory.Cube.Snapshots;

namespace BLUE.ACubeStory.Cube
{
    public class MergeSnapshotFactory_Merge : MergeSnapshotFactoryBase
    {
        [SerializeField] private MergeableComposition _mergeableComposition = null;

        [SerializeField] private MergerBase _merger = null;
        
        public override EMergeSnapshotType GetSnapshotType()
        {
            return EMergeSnapshotType.Merge;
        }

        public override RewindSnapshot_MergeComposition GetSnapshot()
        {
            // return new RewindSnapshot_MergeComposition_Mergeable(_mergeableComposition, _merger, _mergeableComposition.Mergeables[]);

            return null;
        }
    }
}