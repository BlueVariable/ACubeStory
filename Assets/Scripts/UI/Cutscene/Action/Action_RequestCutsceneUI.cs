using System;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class Action_RequestCutsceneUI : InterruptableActionBase
    {
        [SerializeField] private bool _activation = true;
        
        private readonly EventRaiser_OnCutsceneUIRequested _cutsceneUIRequestor = new();
        
        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion
        
        public override void Execute()
        {
            void onResulted(bool result)
            {
                OnCompleted?.Invoke(this);
            }
            
            _cutsceneUIRequestor.Raise(new EventRaiserArgs_OnCutsceneUIRequested(_activation, onResulted));
        }


        public override void Interrupt()
        {
        }
    }
}