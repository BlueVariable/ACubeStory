using BLUE.ACubeStory.Level;

namespace BLUE.ACubeStory.Cube
{
    public class RewindAbilityCondition_LevelState : RewindAbilityConditionBase
    {
        public override bool CanTriggerAbility()
        {
            Level.Level.Instance.LevelFSM.TryGetCurStateType(out ELevelState levelState);

            return levelState == ELevelState.Merge;
        }
    }
}