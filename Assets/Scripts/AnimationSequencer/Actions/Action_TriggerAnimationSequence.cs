using System;
using BLUE.ActionSystem;
using BrunoMikoski.AnimationSequencer;
using UnityEngine;

namespace BLUE.ACubeStory.AnimationSequencer
{
    public class Action_TriggerAnimationSequence : InterruptableActionBase
    {
        [SerializeField] private AnimationSequencerController _animationSequencerController = null;

        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion

        public override void Execute()
        {
            _animationSequencerController.OnFinishedEvent.AddListener(OnAnimationSequenceCompleted);

            _animationSequencerController.Play();
        }

        public override void Interrupt()
        {
            _animationSequencerController.OnFinishedEvent.RemoveListener(OnAnimationSequenceCompleted);

            _animationSequencerController.Kill();
        }

        private void OnAnimationSequenceCompleted()
        {
            OnCompleted?.Invoke(this);
        }
    }
}