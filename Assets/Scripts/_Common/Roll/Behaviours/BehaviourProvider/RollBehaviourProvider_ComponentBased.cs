using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class RollBehaviourProvider_ComponentBased : RollBehaviourProviderBase
    {
        [SerializeField] private Transform _behaviourParent = null;

        public override RollBehaviourBase GetRollBehaviour()
        {
            return _behaviourParent.GetComponentInChildren<RollBehaviourBase>();
        }
    }
}