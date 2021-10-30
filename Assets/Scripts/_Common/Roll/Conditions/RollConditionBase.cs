using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public abstract class RollConditionBase : MonoBehaviour
    {
        public abstract bool CanRoll(Vector3 rollDirection);
    }
}