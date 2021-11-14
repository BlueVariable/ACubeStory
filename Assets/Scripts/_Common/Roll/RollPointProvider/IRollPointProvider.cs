using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public interface IRollPointProvider
    {
        public Vector3 GetRollPoint(Vector3 direction);
    }
}