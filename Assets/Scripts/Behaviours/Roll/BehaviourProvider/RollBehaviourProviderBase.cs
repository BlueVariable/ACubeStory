using UnityEngine;

namespace BLUE.ACubeStory.Behaviours
{
    public abstract class RollBehaviourProviderBase : MonoBehaviour
    {
        public abstract RollBehaviourBase GetRollBehaviour();
    }
}