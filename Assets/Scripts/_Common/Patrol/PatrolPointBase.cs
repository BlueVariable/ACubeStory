using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public abstract class PatrolPointBase : MonoBehaviour
    {
        public abstract Vector3 GetPatrolPointPosition();

        public abstract Quaternion GetPatrolPointRotation();
    }
}