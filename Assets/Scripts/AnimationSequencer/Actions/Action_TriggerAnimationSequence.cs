using System;
using System.Linq;
using BLUE.ActionSystem;
using BLUE.Common;
using BrunoMikoski.AnimationSequencer;
using UnityEngine;

namespace BLUE.ACubeStory.AnimationSequencer
{
    public class Action_TriggerAnimationSequence : InterruptableActionBase
    {
        [SerializeField] private SimpleConditionBase[] _canTriggerSequenceConditions = null;

        [SerializeField] private bool _executeOnce = false;
        
        [SerializeField] private AnimationSequencerController _animationSequencerController = null;

        private bool _isExecutedBefore = false;
        
        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion

        public override void Execute()
        {
            if (_canTriggerSequenceConditions.Any(val => !val.IsConditionSatisfied()))
            {
                OnCompleted?.Invoke(this);

                return;
            }

            if (_executeOnce && _isExecutedBefore)
            {
                OnCompleted?.Invoke(this);

                return;
            }
            
            _animationSequencerController.Play(OnAnimationSequenceCompleted);

            _isExecutedBefore = true;
        }

        public override void Interrupt()
        {
            _animationSequencerController.Kill();
        }

        private void OnAnimationSequenceCompleted()
        {
            OnCompleted?.Invoke(this);
        }
    }
}