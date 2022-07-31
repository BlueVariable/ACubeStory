using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class PatrolPoint_TransformBased : PatrolPointBase
    {
        public override Vector3 GetPatrolPointPosition()
        {
            return transform.position;
        }
    }
}