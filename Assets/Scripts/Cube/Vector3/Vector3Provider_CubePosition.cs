using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Vector3Provider_CubePosition : Vector3ProviderBase
    {
        public override Vector3 GetVector3()
        {
            return Cube.Instance.GetPosition();
        }
    }
}