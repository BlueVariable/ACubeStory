using System;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class Trigger_MovingObjectReachedTarget_Drizzle : TriggerBase
    {
        [SerializeField] private MovingObjectSpawner_Drizzle _movingObjectSpawner = null;

        #region Events

        protected override Action OnTriggerAttempt { get; set; }

        #endregion

        protected override void ActivateCustomActions()
        {
            _movingObjectSpawner.OnMovingObjectReachedTarget += OnMovingObjectReachedTarget;
            
            base.ActivateCustomActions();
        }

        protected override void DeactivateCustomActions()
        {
            _movingObjectSpawner.OnMovingObjectReachedTarget -= OnMovingObjectReachedTarget;

            base.DeactivateCustomActions();
        }

        private void OnMovingObjectReachedTarget()
        {
            OnTriggerAttempt?.Invoke();
        }
    }
}