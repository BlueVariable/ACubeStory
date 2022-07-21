using System.Collections;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Ability;
using BLUE.Common;
using BLUE.InputSystem;
using BLUE.RewindSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Ability_Cube_Rewind : AbilityBase
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;
        [SerializeField] private State_Cube_Rewind _rewindState = null;
        [SerializeField] private RewindController_Cube _rewindController = null;

        [SerializeField] private Transform _canTriggerConditionsObj = null;
        
        [SerializeField] private KeyCode _rewindTriggerKeyCode = KeyCode.R;
        
        [SerializeField] private float _abilityDeactivationDelay = 0.1f;
        
        [SerializeField] private ActionExecutor _rewindAbilityStartedActionExecutor = null;
        [SerializeField] private ActionExecutor _rewindAbilityFinishedActionExecutor = null;
        
        private bool _isRewinding = false;

        private IEnumerator _rewindAbilityDeactivationRoutine;

        private SimpleConditionBase[] _canTriggerConditions;
        private SimpleConditionBase[] _CanTriggerConditions
        {
            get
            {
                if (_canTriggerConditions == null)
                {
                    _canTriggerConditions = _canTriggerConditionsObj.GetComponentsInChildren<SimpleConditionBase>();
                }

                return _canTriggerConditions;
            }
        }

        public override void Attached()
        {
            RegisterToInputManager();
        }

        public override void Detached()
        {
            UnregisterFromInputManager();
            
            UnregisterFromRewindController();

            StopRewindAbilityDeactivationRoutine();
        }

        private void OnDestroy()
        {
            UnregisterFromInputManager();
            
            UnregisterFromRewindController();

            StopRewindAbilityDeactivationRoutine();
        }

        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyDown, OnKeyDown);
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyHold, OnKeyHold);
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyDown, OnKeyDown);
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyHold, OnKeyHold);
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }
        
        private void OnKeyDown(InputData_Key inputData)
        {
            if (inputData.KeyCode == _rewindTriggerKeyCode)
            {
                if (CanTriggerAbility())
                {
                    RegisterToRewindController();
                }

                TryTriggerRewind();
            }
        }

        private void OnKeyHold(InputData_Key inputData)
        {
            if (inputData.KeyCode == _rewindTriggerKeyCode 
                && _rewindController.IsThereAnyAvailableSnapshot())
            {
                TryTriggerRewind();
            }
        }

        private void OnKeyUp(InputData_Key inputData)
        {
            if (inputData.KeyCode == _rewindTriggerKeyCode)
            {
                _rewindState.SetCanContinueRewind(false);
            }
        }

        private void TryTriggerRewind()
        {
            if (!CanTriggerAbility())
            {
                return;
            }
            
            _cubeFSM.TryChangeState(ECubeState.Rewind);
        }

        private bool CanTriggerAbility()
        {
            foreach (SimpleConditionBase condition in _CanTriggerConditions)
            {
                if (!condition.IsConditionSatisfied())
                {
                    return false;
                }
            }
            
            return true;
        }

        private void RegisterToRewindController()
        {
            _rewindController.OnRewindStarted += OnRewindStarted;
            _rewindController.OnRewindCompleted += OnRewindCompleted;
        }

        private void UnregisterFromRewindController()
        {
            _rewindController.OnRewindStarted -= OnRewindStarted;
            _rewindController.OnRewindCompleted -= OnRewindCompleted;
        }

        private void OnRewindStarted()
        {
            if (!_isRewinding)
            {
                _isRewinding = true;
                
                _rewindAbilityStartedActionExecutor?.Execute();
            }
            
            StopRewindAbilityDeactivationRoutine();
        }

        private void OnRewindCompleted(RewindSnapshotBase rewindSnapshot)
        {
            StartRewindAbilityDeactivationRoutine();
        }
        
        private void StartRewindAbilityDeactivationRoutine()
        {
            StopRewindAbilityDeactivationRoutine();

            _rewindAbilityDeactivationRoutine = RewindAbilityDeactivationProgress();

            StartCoroutine(_rewindAbilityDeactivationRoutine);
        }

        private void StopRewindAbilityDeactivationRoutine()
        {
            if (_rewindAbilityDeactivationRoutine != null)
            {
                StopCoroutine(_rewindAbilityDeactivationRoutine);
            }
        }

        private IEnumerator RewindAbilityDeactivationProgress()
        {
            yield return new WaitForSecondsRealtime(_abilityDeactivationDelay);
            
            UnregisterFromRewindController();

            _isRewinding = false;
            
            _rewindAbilityFinishedActionExecutor?.Execute();
        }
    }
}