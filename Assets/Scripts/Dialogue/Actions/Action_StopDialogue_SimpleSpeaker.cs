using System;
using System.Collections;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Dialogue
{
    public class Action_StopDialogue_SimpleSpeaker : InterruptableActionBase
    {
        [SerializeField] private float _initialDelay = 0f;

        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion

        private IEnumerator _dialogueCompleteRoutine;
        
        public override void Execute()
        {
            if (_initialDelay > 0)
            {
                StartDialogueCompleteRoutine();

                return;
            }
            
            StopDialogue();
            
            OnCompleted?.Invoke(this);
        }

        public override void Interrupt()
        {
            StopDialogueCompleteRoutine();
        }

        private void StopDialogue()
        {
            DialoguePlayer_SimpleSpeaker.Instance.Stop();
        }
        
        private void StartDialogueCompleteRoutine()
        {
            StopDialogueCompleteRoutine();

            _dialogueCompleteRoutine = DialogueCompleteProgress();

            StartCoroutine(_dialogueCompleteRoutine);
        }

        private void StopDialogueCompleteRoutine()
        {
            if (_dialogueCompleteRoutine == null)
            {
                return;
            }
            
            StopCoroutine(_dialogueCompleteRoutine);
        }

        private IEnumerator DialogueCompleteProgress()
        {
            yield return new WaitForSeconds(_initialDelay);
            
            StopDialogue();
            
            OnCompleted?.Invoke(this);
        }
    }
}