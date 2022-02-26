using BLUE.ACubeStory.Cube.src;
using BLUE.ACubeStory.Merge;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Rewindable_CubeMergeComposition : RewindableBase<RewindController_Cube, RewindSnapshot_Cube>
    {
        [SerializeField] private MergerBase _merger = null;
        
        private MergeSnapshotFactoryBase[] _snapshotFactories;

        private MergeSnapshotFactoryBase[] _SnapshotFactories
        {
            get
            {
                if (_snapshotFactories == null)
                {
                    _snapshotFactories = GetComponentsInChildren<MergeSnapshotFactoryBase>();
                }

                return _snapshotFactories;
            }
        }
    }
}