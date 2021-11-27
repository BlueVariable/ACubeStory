using BLUE.FSMSystem;

namespace BLUE.ACubeStory.Level
{
    public class State_Level_PreMainMenu : StateBase<ELevelState, FSMTransitionMessage_Level>
    {
        public override ELevelState GetStateType()
        {
            return ELevelState.PreMainMenu;
        }
    }
}