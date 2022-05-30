using System;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Trigger_CubeExitedState : TriggerBase
    {
        [SerializeField] private ECubeState _cubeState = ECubeState.Idle;

        #region Events

        protected override Action OnTriggerAttempt { get; set; }

        #endregion

        protected override void ActivateCustomActions()
        {
            Cube.Instance.CubeFSM.OnStateExited += OnStateExited;
            
            base.ActivateCustomActions();
        }

        protected override void DeactivateCustomActions()
        {
            if (Cube.Instance != null)
            {
                Cube.Instance.CubeFSM.OnStateExited -= OnStateExited;
            }

            base.DeactivateCustomActions();
        }

        private void OnStateExited(ECubeState cubeState)
        {
            if (cubeState == _cubeState)
            {
                OnTriggerAttempt?.Invoke();
            }
        }
    }
}