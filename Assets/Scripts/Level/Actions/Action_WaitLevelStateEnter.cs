using System;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Level
{
    public class Action_WaitLevelStateEnter : InterruptableActionBase
    {
        [SerializeField] private ELevelState _levelState = ELevelState.None;

        [SerializeField] private bool _completeIfStateEnteredAlready = false;
        
        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion
        
        public override void Execute()
        {
            Level.Instance.LevelFSM.TryGetCurStateType(out ELevelState curState);

            if (_completeIfStateEnteredAlready && curState == _levelState)
            {
                OnCompleted?.Invoke(this);

                return;
            }
            
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
            if (Level.Instance == null)
            {
                return;
            }
            
            Level.Instance.LevelFSM.OnStateEntered -= OnStateEntered;
        }
        
        private void OnStateEntered(ELevelState state)
        {
            if (state != _levelState)
            {
                return;
            }

            UnregisterFromLevelFSM();
            
            OnCompleted?.Invoke(this);
        }
    }
}