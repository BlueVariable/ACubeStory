using System.Linq;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Level
{
    public class SimpleCondition_NotInLevelStates : SimpleConditionBase
    {
        [SerializeField] private ELevelState[] _states = new ELevelState[0];

        public override bool IsConditionSatisfied()
        {
            Level.Instance.LevelFSM.TryGetCurStateType(out ELevelState curStateType);

            return !_states.Contains(curStateType);
        }
    }
}