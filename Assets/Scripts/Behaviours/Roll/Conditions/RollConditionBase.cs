using UnityEngine;

namespace BLUE.ACubeStory.Behaviours
{
    public abstract class RollConditionBase : MonoBehaviour
    {
        public abstract bool CanRoll(RollBehaviourBase.ERollDirection rollDirection);
    }
}