using UnityEngine;

namespace BLUE.ACubeStory.Behaviours
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