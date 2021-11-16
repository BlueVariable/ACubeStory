using System;
using BLUE.ActionSystem;

namespace BLUE.ACubeStory.Level
{
    public class Action_WaitPlatformState : InterruptableActionBase
    {
        public override Action<InterruptableActionBase> OnCompleted { get; set; }
    
        public override void Execute()
        {
            RegisterToLevelFSM();
        }

        public override void Interrupt()
        {
            UnregisterFromLevelFSM();
        }

        private void RegisterToLevelFSM()
        {
            Level.Instance.LevelFSM.OnStateEntered += OnStateEntered;
        }

        private void UnregisterFromLevelFSM()
        {
            Level.Instance.LevelFSM.OnStateEntered -= OnStateEntered;
        }
        
        private void OnStateEntered(ELevelState state)
        {
            if (state != ELevelState.Platform)
            {
                return;
            }

            UnregisterFromLevelFSM();
            
            OnCompleted?.Invoke(this);
        }
    }
}