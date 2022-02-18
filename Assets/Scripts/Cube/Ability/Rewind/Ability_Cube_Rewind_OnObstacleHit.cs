using System;
using System.Linq;
using BLUE.ACubeStory.Ability;
using BLUE.ACubeStory.Level;
using BLUE.InputSystem;
using BLUE.RewindSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Ability_Cube_Rewind_OnObstacleHit : AbilityBase
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;

        [SerializeField] private ECubeState _postRewindState = ECubeState.Idle;
        
        [SerializeField] private CubeFailController _cubeFailController = null;

        [SerializeField] private RewindController_FrameBased _rewindController = null;
        
        [SerializeField] private KeyCode _rewindTriggerKeyCode = KeyCode.R;

        [SerializeField] private CubeFailController.ECubeFail[] _cubeFailTypes = default;

        [SerializeField] private TriggerBase[] _checkpointTriggers = default;

        public override void Attached()
        {
            RegisterToLevelFSM();

            RegisterToCheckpointTriggers();
            
            RegisterToCubeFailController();

            ActivateRewindables();
        }

        public override void Detached()
        {
            UnregisterFromLevelFSM();

            UnregisterFromCubeFSM();

            UnregisterFromCubeFailController();

            UnregisterFromCheckpointTriggers();
            
            UnregisterFromInputManager();

            UnregisterFromRewindController();
            
            DeactivateRewindables();
        }

        private void OnDestroy()
        {
            UnregisterFromLevelFSM();
            
            UnregisterFromCubeFSM();

            UnregisterFromCubeFailController();
            
            UnregisterFromCheckpointTriggers();
            
            UnregisterFromInputManager();

            UnregisterFromRewindController();
            
            DeactivateRewindables();
        }

        private void RegisterToLevelFSM()
        {
            Level.Level.Instance.LevelFSM.TryGetCurStateType(out ELevelState curState);

            if (curState == ELevelState.Platform)
            {
                OnLevelStateEntered(ELevelState.Platform);

                return;
            }
            
            Level.Level.Instance.LevelFSM.OnStateEntered += OnLevelStateEntered;
            Level.Level.Instance.LevelFSM.OnStateExited += OnLevelStateExited;
        }
        
        private void UnregisterFromLevelFSM()
        {
            if (Level.Level.Instance == null)
            {
                return;
            }
            
            Level.Level.Instance.LevelFSM.OnStateEntered -= OnLevelStateEntered;
            Level.Level.Instance.LevelFSM.OnStateExited -= OnLevelStateExited;
        }
        
        private void RegisterToCubeFSM()
        {
            _cubeFSM.OnStateEntered += OnCubeStateEntered;
        }

        private void UnregisterFromCubeFSM()
        {
            _cubeFSM.OnStateEntered -= OnCubeStateEntered;
        }

        private void RegisterToCheckpointTriggers()
        {
            Array.ForEach(_checkpointTriggers, val => val.OnTriggered += OnCheckpointTriggered);
        }

        private void UnregisterFromCheckpointTriggers()
        {
            Array.ForEach(_checkpointTriggers, val => val.OnTriggered -= OnCheckpointTriggered);
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
        
        private void OnLevelStateEntered(ELevelState levelState)
        {
            if (levelState.Equals(ELevelState.Platform))
            {
                UnregisterFromCubeFSM();
                
                RegisterToCubeFSM();
            }
        }
        
        private void OnLevelStateExited(ELevelState levelState)
        {
            if (levelState.Equals(ELevelState.Platform))
            {
                _rewindController.StopSaveSnapshotRoutine();
                
                _rewindController.ResetSnapshots();
                
                UnregisterFromLevelFSM();
            
                UnregisterFromCubeFSM();

                UnregisterFromCubeFailController();
                
                UnregisterFromCheckpointTriggers();
            
                UnregisterFromInputManager();

                UnregisterFromRewindController();
            
                DeactivateRewindables();
            }
        }
        
        private void OnCheckpointTriggered(TriggerBase trigger)
        {
            UnregisterFromCubeFSM();
            
            RegisterToCubeFSM();
        }
        
        private void OnCubeStateEntered(ECubeState cubeState)
        {
            if (cubeState == ECubeState.Idle)
            {
                _rewindController.StopSaveSnapshotRoutine();
                
                _rewindController.ResetSnapshots();
                
                _rewindController.StartSaveSnapshotRoutine();
                
                UnregisterFromCubeFSM();
            }
        }

        private void OnFailTriggered(CubeFailController.ECubeFail cubeFailType, TriggerBase trigger)
        {
            _cubeFSM.TryGetCurStateType(out ECubeState curCubeState);

            if (curCubeState == ECubeState.Rewind)
            {
                return;
            }
            
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

            _cubeFSM.TryChangeState(_postRewindState);

            _rewindController.ResetSnapshots();
                
            _rewindController.StartSaveSnapshotRoutine();
        }
    }
}