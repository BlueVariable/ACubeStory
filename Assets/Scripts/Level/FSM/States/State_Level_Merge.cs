using BLUE.FSMSystem;

namespace BLUE.ACubeStory.Level
{
    public class State_Level_Merge : StateBase<ELevelState, FSMTransitionMessage_Level>
    {
        public override ELevelState GetStateType()
        {
            return ELevelState.Merge;
        }
    }
}