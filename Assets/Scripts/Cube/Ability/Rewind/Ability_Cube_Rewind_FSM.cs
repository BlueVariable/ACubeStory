using System.Collections;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Ability;
using BLUE.ACubeStory.Cube.src;
using BLUE.InputSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Ability_Cube_Rewind_FSM : AbilityBase
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;
        [SerializeField] private State_Cube_Rewind _rewindState = null;
        [SerializeField] private RewindController_Cube _rewindController = null;
        
        [SerializeField] private KeyCode _rewindTriggerKeyCode = KeyCode.R;

        [SerializeField] private ActionExecutor _rewindStartedActions = null;
        [SerializeField] private ActionExecutor _rewindCompletedActions = null;
        
        private bool _isInRewind = false;
        private bool _isRewindContinuous = false;

        private IEnumerator _rewindControlRoutine;
        
        public override void Attached()
        {
            RegisterToInputManager();
        }

        public override void Detached()
        {
            UnregisterFromInputManager();
        }

        private void OnDestroy()
        {
            UnregisterFromInputManager();
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
                RegisterToRewindController();

                _cubeFSM.TryChangeState(ECubeState.Rewind);

                _isRewindContinuous = true;
            }
        }

        private void OnKeyHold(InputData_Key inputData)
        {
            _isRewindContinuous = _isRewindContinuous && _rewindController.IsThereAnyAvailableSnapshot();
            
            if (inputData.KeyCode == _rewindTriggerKeyCode && _isRewindContinuous)
            {
                _cubeFSM.TryChangeState(ECubeState.Rewind);
            }
        }

        private void OnKeyUp(InputData_Key inputData)
        {
            if (inputData.KeyCode == _rewindTriggerKeyCode)
            {
                _rewindState.TriggerExitRewindState();
            }
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
            if (!_isInRewind)
            {
                _isInRewind = true;
                
                _rewindStartedActions?.Execute();
            }
            
            StopRewindControlRoutine();
        }

        private void OnRewindCompleted(RewindSnapshotBase rewindSnapshot)
        {
            StartRewindControlRoutine();
        }
        
        private void StartRewindControlRoutine()
        {
            StopRewindControlRoutine();

            _rewindControlRoutine = RewindControlProgress();

            StartCoroutine(_rewindControlRoutine);
        }

        private void StopRewindControlRoutine()
        {
            if (_rewindControlRoutine != null)
            {
                StopCoroutine(_rewindControlRoutine);
            }
        }

        private IEnumerator RewindControlProgress()
        {
            yield return new WaitForSecondsRealtime(0.1f);
            
            UnregisterFromRewindController();

            _isInRewind = false;
            
            _rewindCompletedActions?.Execute();
        }
    }
}