using UnityEngine;

namespace BLUE.ACubeStory.Behaviours
{
    public interface IRollPointProvider
    {
        public abstract Vector3 GetRollPoint(RollBehaviourBase.ERollDirection direction);
    }
}