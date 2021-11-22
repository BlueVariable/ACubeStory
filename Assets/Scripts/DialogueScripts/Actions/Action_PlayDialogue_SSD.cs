using System;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Dialogue.Actions
{
    public class Action_PlayDialogue_SSD : InterruptableActionBase
    {
        [SerializeField] private ESpeakerDialogue_SSD _dialogue = ESpeakerDialogue_SSD.None;

        [SerializeField] private bool _autoStopOnComplete = true;
        
        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        public override void Execute()
        {
            DialoguePlayer_SSD.Instance.Play(
                _dialogue,
                () => OnCompleted?.Invoke(this),
                _autoStopOnComplete);
        }

        public override void Interrupt()
        {
            DialoguePlayer_SSD.Instance.Stop();
        }
    }
}