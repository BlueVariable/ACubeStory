using System.Linq;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class SimpleCondition_Cube_StateSatisfied : SimpleConditionBase
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;

        [SerializeField] private ECubeState[] _targetStates = default;
        
        public override bool IsConditionSatisfied()
        {
            bool isStateValid = _cubeFSM.TryGetCurStateType(out ECubeState cubeState);

            if (!isStateValid)
            {
                return false;
            }

            return _targetStates.Contains(cubeState);
        }
    }
}