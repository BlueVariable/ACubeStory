using System;
using BLUE.Helpers;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Trigger_CubeFail_FallTowardsVoid : Trigger_CubeFailBase
    {
        [SerializeField] private State_Cube_Fall _cubeFallState = null;

        [SerializeField] private float _triggerDelay = 2f;
        
        #region Events

        public override Action<Trigger_CubeFailBase> OnFailStarted { get; set; }
        
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

            OnFailStarted?.Invoke(this);
            
            CoroutineHelper.Instance.WaitForSeconds(_triggerDelay, onDelayFinished);
        }

    }
}