using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Moth
{
    public class TransformProvider_CubeMothLandReference : TransformProviderBase
    {
        public override Transform GetTransform()
        {
            return CubeMothLandReference.Instance.transform;
        }
    }
}