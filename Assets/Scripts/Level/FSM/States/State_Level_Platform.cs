using BLUE.ACubeStory.UI;
using BLUE.FSMSystem;

namespace BLUE.ACubeStory.Level
{
    public class State_Level_Platform : StateBase<ELevelState, FSMTransitionMessage_Level>
    {
        public override ELevelState GetStateType()
        {
            return ELevelState.Platform;
        }

        private void Awake()
        {
            EventRaiser_OnMainMenuButtonTriggered.OnRaisedStatic += onMainMenuButtonEntered;

            void onMainMenuButtonEntered(object sender, EventArgs_OnMainMenuButtonTriggered args)
            {
                if (!args.Button.Equals(EMainMenuButton.Play))
                {
                    return;
                }
                
                EventRaiser_OnMainMenuButtonTriggered.OnRaisedStatic -= onMainMenuButtonEntered;

                Level.Instance.LevelFSM.TryChangeState(ELevelState.Platform);
            }
        }
    }
}