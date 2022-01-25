using System;
using System.Collections;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Dialogue
{
    public class Action_StopDialogue_SimpleSpeaker : InterruptableActionBase
    {
        [SerializeField] private float _initialDelay = 0f;
    
        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        private IEnumerator _waitRoutine;
        
        public override void Execute()
        {
            if (_initialDelay > 0)
            {
                StartWaitRoutine();

                return;
            }
            
            StopDialogue();
            
            OnCompleted?.Invoke(this);
        }

        public override void Interrupt()
        {
            StopWaitRoutine();
        }

        private void StopDialogue()
        {
            DialoguePlayer_SimpleSpeaker.Instance.Stop();
        }
        
        private void StartWaitRoutine()
        {
            StopWaitRoutine();

            _waitRoutine = WaitProgress();

            StartCoroutine(_waitRoutine);
        }

        private void StopWaitRoutine()
        {
            if (_waitRoutine == null)
            {
                return;
            }
            
            StopCoroutine(_waitRoutine);
        }

        private IEnumerator WaitProgress()
        {
            yield return new WaitForSeconds(_initialDelay);
            
            StopDialogue();
            
            OnCompleted?.Invoke(this);
        }
    }
}