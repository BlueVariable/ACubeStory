using System;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Dialogue.Actions
{
    public class Action_PlayDialogue_SSD : InterruptableActionBase
    {
        [SerializeField] private ESpeakerDialogue_SSD[] _dialogueOptions = new ESpeakerDialogue_SSD[0];

        [SerializeField] [Range(0, 1)] private float _dialoguePlayProbability = 1f;
        
        [SerializeField] private bool _autoStopOnComplete = true;
        
        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        public override void Execute()
        {
            if (!CanPlay())
            {
                OnCompleted?.Invoke(this);

                return;
            }
            
            ESpeakerDialogue_SSD dialogue = _dialogueOptions[UnityEngine.Random.Range(0, _dialogueOptions.Length)];
            
            DialoguePlayer_SSD.Instance.Play(
                dialogue,
                () => OnCompleted?.Invoke(this),
                _autoStopOnComplete);
        }

        public override void Interrupt()
        {
            DialoguePlayer_SSD.Instance.Stop();
        }

        private bool CanPlay()
        {
            return _dialoguePlayProbability > UnityEngine.Random.Range(0, 1);
        }
    }
}