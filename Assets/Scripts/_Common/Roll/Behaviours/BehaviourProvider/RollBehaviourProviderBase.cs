using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public abstract class RollBehaviourProviderBase : MonoBehaviour
    {
        public abstract RollBehaviourBase GetRollBehaviour();
    }
}