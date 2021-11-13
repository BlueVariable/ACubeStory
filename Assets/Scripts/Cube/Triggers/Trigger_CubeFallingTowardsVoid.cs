using System;
using BLUE.Helpers;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Trigger_CubeFallingTowardsVoid : TriggerBase
    {
        [SerializeField] private State_Cube_Fall _cubeFallState = null;

        [SerializeField] private float _triggerDelay = 2f;
        
        #region Events

        protected override Action OnTriggerAttempt { get; set; }

        #endregion

        protected override void ActivateCustomActions()
        {
            _cubeFallState.OnFallStartedTowardsVoid += OnFallStartedTowardsVoid;
            
            base.ActivateCustomActions();
        }

        protected override void DeactivateCustomActions()
        {
            _cubeFallState.OnFallStartedTowardsVoid -= OnFallStartedTowardsVoid;
            
            base.DeactivateCustomActions();
        }

        private void OnFallStartedTowardsVoid()
        {
            void onDelayFinished()
            {
                OnTriggerAttempt?.Invoke();
            }
            
            CoroutineHelper.Instance.WaitForSeconds(_triggerDelay, onDelayFinished);
        }

    }
}