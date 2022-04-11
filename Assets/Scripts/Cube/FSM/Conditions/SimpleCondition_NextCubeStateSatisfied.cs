using System.Linq;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class SimpleCondition_NextCubeStateSatisfied : SimpleConditionBase
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;

        [SerializeField] private ECubeState[] _targetStates = default;
        
        public override bool IsConditionSatisfied()
        {
            return _targetStates.Contains(_cubeFSM.NextState);
        }
    }
}