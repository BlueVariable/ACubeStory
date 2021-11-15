using BLUE.FSMSystem;

namespace BLUE.ACubeStory.Level
{
    public class FSMTransition_Level : FSMTransitionBase<ELevelState>
    {
        public FSMTransition_Level(ELevelState initialStateType, ELevelState targetStateType) 
            : base(initialStateType, targetStateType)
        {
        }
    }
}