using BLUE.ACubeStory.Common;
using BLUE.ACubeStory.Platform;
using BLUE.FSMSystem;

namespace BLUE.ACubeStory.Cube
{
    public class StateEnterCondition_AllPlatformsStabil : StateEnterConditionBase
    {
        public override bool CanStateBeEntered()
        {
            if (PlatformContainer.Instance == null)
            {
                return true;
            }

            foreach (Platform.Platform platform in PlatformContainer.Instance.Platforms)
            {
                PatrolBehaviourBase patrolBehaviour = platform.GetComponentInChildren<PatrolBehaviourBase>();
                
                if (patrolBehaviour == null)
                {
                    continue;
                }

                if (patrolBehaviour.IsPatrolling)
                {
                    return false;
                }
            }

            return true;
        }
    }
}