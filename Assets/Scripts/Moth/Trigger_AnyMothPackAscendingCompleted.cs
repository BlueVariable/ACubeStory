using System;
using BLUE.TriggerSystem;

namespace BLUE.ACubeStory.Moth
{
    public class Trigger_AnyMothPackAscendingCompleted : TriggerBase
    {
        #region Events

        protected override Action OnTriggerAttempt { get; set; }

        #endregion

        protected override void ActivateCustomActions()
        {
            EventRaiser_OnMothPackAscendingCompleted.OnRaisedStatic += OnMothPackAscendingCompleted;
            
            base.ActivateCustomActions();
        }

        protected override void DeactivateCustomActions()
        {
            EventRaiser_OnMothPackAscendingCompleted.OnRaisedStatic -= OnMothPackAscendingCompleted;
            
            base.DeactivateCustomActions();
        }

        private void OnMothPackAscendingCompleted(object sender, EventRaiserArgs_OnMothPackAscendingCompleted e)
        {
            OnTriggerAttempt?.Invoke();
        }
    }
}