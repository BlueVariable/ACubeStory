using BLUE.ACubeStory.Level;
using BLUE.UISystem;

namespace BLUE.ACubeStory.UI
{
    public class UI_MainMenu : UIBase
    {
        protected override void AwakeCustomActions()
        {
            Level.Level.Instance.LevelFSM.TryGetCurStateType(out ELevelState curState);

            if (curState == ELevelState.MainMenu)
            {
                TryActivate();
            }
            
            RegisterToLevel();
            
            base.AwakeCustomActions();
        }

        protected override void DestroyCustomActions()
        {
            UnregisterFromLevel();
            
            base.DestroyCustomActions();
        }

        private void RegisterToLevel()
        {
            Level.Level.Instance.LevelFSM.OnStateEntered += OnStateEntered;
            Level.Level.Instance.LevelFSM.OnStateExited += OnStateExited;
        }

        private void UnregisterFromLevel()
        {
            if (!Level.Level.Instance)
            {
                return;
            }
            
            Level.Level.Instance.LevelFSM.OnStateEntered -= OnStateEntered;
            Level.Level.Instance.LevelFSM.OnStateExited -= OnStateExited;
        }
        
        private void OnStateEntered(ELevelState levelState)
        {
            if (levelState != ELevelState.MainMenu)
            {
                return;
            }

            TryActivate();
        }
        
        private void OnStateExited(ELevelState levelState)
        {
            if (levelState != ELevelState.MainMenu)
            {
                return;
            }

            TryDeactivate();
        }
    }
}