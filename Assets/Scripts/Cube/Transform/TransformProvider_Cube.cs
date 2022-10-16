using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class TransformProvider_Cube : TransformProviderBase
    {
        public override Transform GetTransform()
        {
            return Cube.Instance.transform;
        }
    }
}