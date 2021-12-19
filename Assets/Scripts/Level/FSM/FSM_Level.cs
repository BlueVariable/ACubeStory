using System.Collections.Generic;
using BLUE.FSMSystem;

namespace BLUE.ACubeStory.Level
{
    public class FSM_Level : FSMBase<ELevelState, FSMTransitionMessage_Level, FSMTransition_Level>
    {
        protected override List<FSMTransition_Level> GetInitialPossibleTransitions()
        {
            return new List<FSMTransition_Level>
            {
                new FSMTransition_Level(ELevelState.Platform, ELevelState.Merge),
                
                new FSMTransition_Level(ELevelState.Merge, ELevelState.Platform),
            };
        }
    }
}