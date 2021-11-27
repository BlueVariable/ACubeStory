using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Level
{
    public class Action_ChangeLevelState : ActionBase
    {
        [SerializeField] private ELevelState _state = ELevelState.None;
        
        public override void Execute()
        {
            Level.Instance.LevelFSM.TryChangeState(_state, new FSMTransitionMessage_Level());
        }
    }
}