using System;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Trigger_CubeFail_ObstacleHit : Trigger_CubeFailBase
    {
        [SerializeField] private Trigger_OnTriggerEnter _obstacleHitTrigger = null;

        #region Events

        public override Action<Trigger_CubeFailBase> OnFailStarted { get; set; }
        
        protected override Action OnTriggerAttempt { get; set; }

        #endregion

        protected override void ActivateCustomActions()
        {
            _obstacleHitTrigger.OnTriggered += OnObstacleHitTriggered;

            base.ActivateCustomActions();
        }

        protected override void DeactivateCustomActions()
        {
            _obstacleHitTrigger.OnTriggered -= OnObstacleHitTriggered;
            
            base.DeactivateCustomActions();
        }

        private void OnObstacleHitTriggered(TriggerBase obstacleHitTrigger)
        {
            OnFailStarted?.Invoke(this);
            
            OnTriggerAttempt?.Invoke();
        }
    }
}