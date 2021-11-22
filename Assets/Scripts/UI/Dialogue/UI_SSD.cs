using System;
using System.Collections.Generic;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Dialogue;
using BLUE.DialogueSystem;
using BLUE.TriggerSystem;
using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class UI_SSD : UIBase
    {
        [Header("Dialogue Settings")]
        [SerializeField] private UI_Text _dialogueTextUI = null;

        [SerializeField] private TriggerBase _dialogueContinueTrigger = null;

        [Header("Speaker Settings")]
        [SerializeField] private bool _isCustomSpeakerUI = false;
        [SerializeField] private ESpeaker_SSD _speaker = ESpeaker_SSD.None;

        [SerializeField] private ActionExecutor _dialogueDisplayingActions = null;
        [SerializeField] private ActionExecutor _dialogueDisplayedActions = null;
        
        private Queue<string> _dialogueQueue;

        private Action _onCompletedCallback;

        public ESpeaker_SSD LastSpeakingSpeaker { get; private set; }

        protected override void AwakeCustomActions()
        {
            EventRaiser_OnDialogueActivationRequested<DialogueData_SSD>.OnRaisedStatic += OnNarrativeActivationRequested;
            EventRaiser_OnDialogueDeactivationRequested.OnRaisedStatic += OnNarrativeDeactivationRequested;
            
            base.AwakeCustomActions();
        }

        protected override void DestroyCustomActions()
        {
            EventRaiser_OnDialogueActivationRequested<DialogueData_SSD>.OnRaisedStatic -= OnNarrativeActivationRequested;
            EventRaiser_OnDialogueDeactivationRequested.OnRaisedStatic -= OnNarrativeDeactivationRequested;

            base.DestroyCustomActions();
        }

        protected override void ActivatedCustomActions()
        {
            DisplayNextInQueue();
            
            base.ActivatedCustomActions();
        }

        private void OnNarrativeActivationRequested(
            object sender, 
            EventArgs_OnDialogueActivationRequested<DialogueData_SSD> e)
        {
            if (_isCustomSpeakerUI && _speaker != e.DialogueData.Speaker)
            {
                return;
            }
            
            LastSpeakingSpeaker = e.DialogueData.Speaker;

            _dialogueQueue = new Queue<string>(e.DialogueData.SpeakerText);

            _onCompletedCallback = e.OnCompletedCallback;

            if (Status != EStatus.Activated)
            {
                TryActivate();
            }
            else
            {
                DisplayNextInQueue();
            }
        }

        private void OnNarrativeDeactivationRequested(object sender, EventArgs_OnDialogueDeactivationRequested e)
        {
            TryDeactivate();
        }
        
        private void DisplayNextInQueue()
        {
            void onContinued(TriggerBase triggerBase)
            {
                _dialogueContinueTrigger.OnTriggered -= onContinued;

                DisplayNextInQueue();
            }
            
            void onTextDisplayed()
            {
                _dialogueTextUI.OnTextDisplayed -= onTextDisplayed;

                _dialogueContinueTrigger.OnTriggered += onContinued;
                
                _dialogueDisplayedActions.Execute();
            }

            if (_dialogueQueue.Count == 0)
            {
                _onCompletedCallback?.Invoke();

                return;
            }
            
            _dialogueTextUI.OnTextDisplayed += onTextDisplayed;
            
            _dialogueTextUI.Text = _dialogueQueue.Dequeue();
            
            _dialogueDisplayingActions.Execute();
        }
    }
}