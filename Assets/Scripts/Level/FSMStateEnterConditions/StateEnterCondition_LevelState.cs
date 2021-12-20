using System.Linq;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Level
{
    public class StateEnterCondition_LevelState : StateEnterConditionBase
    {
        [SerializeField] private ELevelState[] _levelStates = new ELevelState[0];
        
        public override bool CanStateBeEntered()
        {
            Level.Instance.LevelFSM.TryGetCurStateType(out ELevelState curStateType);

            return _levelStates.Contains(curStateType);
        }
    }
}