using BLUE.ACubeStory.Merge;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Dialogue
{
    public class Vector3Provider_MergeableCompositionBased_Highest : Vector3ProviderBase
    {
        [SerializeField] private MergeableComposition _mergeableComposition = null;

        [SerializeField] private Vector3 _offset = default;
        
        public override Vector3 GetVector3()
        {
            Bounds bounds = _mergeableComposition.GetBounds();

            return new Vector3(bounds.center.x, bounds.max.y, bounds.center.z) + _offset;
        }
    }
}