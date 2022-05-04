using System;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class Trigger_MovingObjectSequenceCompleted_Drizzle : TriggerBase
    {
        [SerializeField] private MovingObjectSpawner_Drizzle _movingObjectSpawner = null;

        #region Events

        protected override Action OnTriggerAttempt { get; set; }

        #endregion

        protected override void ActivateCustomActions()
        {
            _movingObjectSpawner.OnMovingObjectSequenceCompleted += OnMovingObjectSequenceCompleted;
            
            base.ActivateCustomActions();
        }

        protected override void DeactivateCustomActions()
        {
            _movingObjectSpawner.OnMovingObjectSequenceCompleted -= OnMovingObjectSequenceCompleted;

            base.DeactivateCustomActions();
        }

        private void OnMovingObjectSequenceCompleted(MovingObjectBase movingObject)
        {
            OnTriggerAttempt?.Invoke();
        }
    }
}