using BLUE.ActionSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class MainMenuButton : MonoBehaviour
    {
        [Header("Select Settings")]
        [SerializeField] private TriggerBase _selectTrigger = null;
        [SerializeField] private ActionExecutor _selectedActions = null;
        [SerializeField] private TriggerBase _unselectTrigger = null;
        [SerializeField] private ActionExecutor _unselectedActions = null;
        
        [Header("Enter Settings")]
        [SerializeField] private bool _waitSelectToEnter = false;
        [SerializeField] private TriggerBase _enterTrigger = null;
        [SerializeField] private ActionExecutor _enteredActions = null;
        [SerializeField] private TriggerBase _unenterTrigger = null;
        [SerializeField] private ActionExecutor _unenteredActions = null;

        public bool IsSelected { get; private set; }
        
        private void Awake()
        {
            RegisterToTriggers();
        }

        private void OnDestroy()
        {
            UnregisterFromTriggers();
        }

        private void RegisterToTriggers()
        {
            if (_selectTrigger)
            {
                _selectTrigger.OnTriggered += OnSelected;
            }
            
            if (_unselectTrigger)
            {
                _unselectTrigger.OnTriggered += OnUnselected;
            }

            if (_enterTrigger)
            {
                _enterTrigger.OnTriggered += OnEntered;
            }
            
            if (_unenterTrigger)
            {
                _unenterTrigger.OnTriggered += OnUnentered;
            }
        }

        private void UnregisterFromTriggers()
        {
            if (_selectTrigger)
            {
                _selectTrigger.OnTriggered -= OnSelected;
            }
            
            if (_unselectTrigger)
            {
                _unselectTrigger.OnTriggered -= OnUnselected;
            }

            if (_enterTrigger)
            {
                _enterTrigger.OnTriggered -= OnEntered;
            }
            
            if (_unenterTrigger)
            {
                _unenterTrigger.OnTriggered -= OnUnentered;
            }
        }
        
        private void OnSelected(TriggerBase trigger)
        {
            IsSelected = true;
            
            _selectedActions.Execute();
        }
        
        private void OnUnselected(TriggerBase trigger)
        {
            IsSelected = false;
            
            _unselectedActions.Execute();
        }
        
        private void OnEntered(TriggerBase trigger)
        {
            if (_waitSelectToEnter && !IsSelected)
            {
                return;
            }
            
            _enteredActions.Execute();
        }
        
        private void OnUnentered(TriggerBase trigger)
        {
            _unenteredActions.Execute();
        }
    }
}