using System;
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
            EventRaiser_OnMainMenuButtonEntered.OnRaisedStatic += onMainMenuButtonEntered;

            void onMainMenuButtonEntered(object sender, EventArgs_OnMainMenuButtonEntered args)
            {
                if (!args.Button.Equals(EMainMenuButton.Play))
                {
                    return;
                }
                
                EventRaiser_OnMainMenuButtonEntered.OnRaisedStatic -= onMainMenuButtonEntered;

                Level.Instance.LevelFSM.TryChangeState(ELevelState.Platform);
            }
        }
    }
}