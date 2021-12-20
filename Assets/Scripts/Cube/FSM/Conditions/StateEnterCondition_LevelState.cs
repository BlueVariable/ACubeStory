using System.Linq;
using BLUE.ACubeStory.Level;
using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class StateEnterCondition_LevelState : StateEnterConditionBase
    {
        [SerializeField] private ELevelState[] _levelStates = new ELevelState[0];
        
        public override bool CanStateBeEntered()
        {
            Level.Level.Instance.LevelFSM.TryGetCurStateType(out ELevelState curStateType);

            return _levelStates.Contains(curStateType);
        }
    }
}