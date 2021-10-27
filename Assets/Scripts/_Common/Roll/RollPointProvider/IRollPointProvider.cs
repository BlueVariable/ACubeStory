using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public interface IRollPointProvider
    {
        public abstract Vector3 GetRollPoint(RollBehaviourBase.ERollDirection direction);
    }
}