using System.Collections;
using System.Linq;
using BLUE.ACubeStory.Ability;
using BLUE.InputSystem;
using BLUE.RewindSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Ability_Cube_Rewind_OnFall : AbilityBase
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;

        [SerializeField] private CubeFailController _cubeFailController = null;

        [SerializeField] private RewindController _rewindController = null;
        
        [SerializeField] private KeyCode _rewindTriggerKeyCode = KeyCode.R;

        [SerializeField] private ECubeState _rewindAnchorState = ECubeState.Idle;

        [SerializeField] private CubeFailController.ECubeFail[] _cubeFailTypes = default;

        [SerializeField] private int _anchorStateIdenticalSnapshotTolerance = 3;
        
        private IEnumerator _snapshotToleranceRoutine;
        
        public override void Attached()
        {
            RegisterToCubeFSM();

            RegisterToCubeFailController();

            ActivateRewindables();
        }

        public override void Detached()
        {
            StopSnapshotToleranceRoutine();
            
            UnregisterFromCubeFSM();

            UnregisterFromCubeFailController();
            
            UnregisterFromInputManager();

            UnregisterFromRewindController();
            
            DeactivateRewindables();
        }

        private void OnDestroy()
        {
            StopSnapshotToleranceRoutine();
            
            UnregisterFromCubeFSM();

            UnregisterFromCubeFailController();
            
            UnregisterFromInputManager();

            UnregisterFromRewindController();
            
            DeactivateRewindables();
        }

        private void RegisterToCubeFSM()
        {
            _cubeFSM.OnStateEntered += OnCubeStateEntered;
            _cubeFSM.OnStateExited += OnCubeStateExited;
        }

        private void UnregisterFromCubeFSM()
        {
            _cubeFSM.OnStateEntered -= OnCubeStateEntered;
            _cubeFSM.OnStateExited -= OnCubeStateExited;
        }

        private void RegisterToCubeFailController()
        {
            _cubeFailController.OnFailTriggered += OnFailTriggered;
        }
        
        private void UnregisterFromCubeFailController()
        {
            _cubeFailController.OnFailTriggered -= OnFailTriggered;
        }

        private void ActivateRewindables()
        {
            IRewindable[] rewindables = _rewindController.GetComponentsInChildren<IRewindable>();

            foreach (IRewindable rewindable in rewindables)
            {
                rewindable.ActivateRewindable(_rewindController);
            }
        }

        private void DeactivateRewindables()
        {
            IRewindable[] rewindables = _rewindController.GetComponentsInChildren<IRewindable>();

            foreach (IRewindable rewindable in rewindables)
            {
                rewindable.DeactivateRewindable(_rewindController);
            }
        }
        
        private void OnCubeStateEntered(ECubeState cubeState)
        {
            if (cubeState == _rewindAnchorState)
            {
                _rewindController.StopSaveSnapshotRoutine();
                
                _rewindController.ResetSnapshots();
                
                _rewindController.StartSaveSnapshotRoutine();
                
                StartSnapshotToleranceRoutine();
            }
        }
        
        private void OnCubeStateExited(ECubeState cubeState)
        {
            if (cubeState == _rewindAnchorState)
            {
                StopSnapshotToleranceRoutine();
                
                _rewindController.StopSaveSnapshotRoutine();

                _rewindController.StartSaveSnapshotRoutine();
            }
        }

        private void StartSnapshotToleranceRoutine()
        {
            StopSnapshotToleranceRoutine();

            _snapshotToleranceRoutine = SnapshotToleranceProgress();

            StartCoroutine(_snapshotToleranceRoutine);
        }

        private void StopSnapshotToleranceRoutine()
        {
            if (_snapshotToleranceRoutine != null)
            {
                StopCoroutine(_snapshotToleranceRoutine);
            }
        }

        private IEnumerator SnapshotToleranceProgress()
        {
            int count = 0;
            
            while (count < _anchorStateIdenticalSnapshotTolerance)
            {
                count++;
                
                yield return null;
            }
            
            _rewindController.StopSaveSnapshotRoutine();
        }

        private void OnFailTriggered(CubeFailController.ECubeFail cubeFailType, TriggerBase trigger)
        {
            if (_cubeFailTypes.Contains(cubeFailType))
            {
                _cubeFSM.TryChangeState(ECubeState.Rewind);

                _rewindController.StopSaveSnapshotRoutine();
                
                RegisterToInputManager();
            }
        }

        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyDown, OnKeyDown);
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyDown, OnKeyDown);
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyUp, OnKeyUp);
        }

        private void OnKeyDown(InputData_Key inputData)
        {
            if (inputData.KeyCode == _rewindTriggerKeyCode)
            {
                RegisterToRewindController();
                
                _rewindController.StartRewind();
            }
        }
        
        private void OnKeyUp(InputData_Key inputData)
        {
            if (inputData.KeyCode == _rewindTriggerKeyCode)
            {
                UnregisterFromRewindController();
                
                _rewindController.StopRewind();
            }
        }

        private void RegisterToRewindController()
        {
            _rewindController.OnRewindableRewound += OnRewindableRewound;
        }
        
        private void UnregisterFromRewindController()
        {
            _rewindController.OnRewindableRewound -= OnRewindableRewound;
        }

        private void OnRewindableRewound(IRewindable rewindable)
        {
            UnregisterFromRewindController();
            
            UnregisterFromInputManager();
            
            _rewindController.StopRewind();

            _cubeFSM.TryChangeState(_rewindAnchorState);
        }
    }
}