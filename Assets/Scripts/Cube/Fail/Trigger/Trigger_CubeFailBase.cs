using System;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public abstract class Trigger_CubeFailBase : TriggerBase
    {
        [field: SerializeField] public ECubeFail CubeFailType {get; private set; }
        
        #region Events

        public abstract Action<Trigger_CubeFailBase> OnFailStarted { get; set; }
        
        protected override Action OnTriggerAttempt { get; set; }


        #endregion

        protected override void ActivateCustomActions()
        {
            CubeFailController.Instance.RegisterTrigger(this);
            
            base.ActivateCustomActions();
        }

        protected override void DeactivateCustomActions()
        {
            CubeFailController.Instance.UnregisterTrigger(this);
            
            base.DeactivateCustomActions();
        }
    }
}