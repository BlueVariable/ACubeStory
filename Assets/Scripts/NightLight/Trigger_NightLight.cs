using System;
using BLUE.TriggerSystem;
using UnityEngine;
using ColorTweener = BLUE.Common.MaterialPropertyBlockBehaviour_ColorTween;

namespace BLUE.ACubeStory.NightLight
{
    public class Trigger_NightLight : TriggerBase
    {
        protected override Action OnTriggerAttempt { get; set; }

        public void Trigger()
        {
            OnTriggerAttempt?.Invoke();
        }
    }
}