using System;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Trigger_CubeEnteredState : TriggerBase
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;

        [SerializeField] private ECubeState _cubeState = ECubeState.Idle;

        #region Events

        protected override Action OnTriggerAttempt { get; set; }

        #endregion

        protected override void ActivateCustomActions()
        {
            _cubeFSM.OnStateEntered += OnStateEntered;
            
            base.ActivateCustomActions();
        }

        protected override void DeactivateCustomActions()
        {
            _cubeFSM.OnStateEntered -= OnStateEntered;
            
            base.DeactivateCustomActions();
        }

        private void OnStateEntered(ECubeState cubeState)
        {
            if (cubeState == _cubeState)
            {
                OnTriggerAttempt?.Invoke();
            }
        }
    }
}
