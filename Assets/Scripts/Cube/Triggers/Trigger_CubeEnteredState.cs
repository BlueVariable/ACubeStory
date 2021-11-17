using System;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Trigger_CubeEnteredState : TriggerBase
    {
        [SerializeField] private ECubeState _cubeState = ECubeState.Idle;

        #region Events

        protected override Action OnTriggerAttempt { get; set; }

        #endregion

        protected override void ActivateCustomActions()
        {
            Cube.Instance.CubeFSM.OnStateEntered += OnStateEntered;
            
            base.ActivateCustomActions();
        }

        protected override void DeactivateCustomActions()
        {
            if (Cube.Instance != null)
            {
                Cube.Instance.CubeFSM.OnStateEntered -= OnStateEntered;
            }

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
