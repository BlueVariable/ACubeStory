using System.Linq;
using BLUE.ACubeStory.Ability;
using BLUE.ACubeStory.Level;
using BLUE.ACubeStory.Rewind;
using BLUE.InputSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Ability_Cube_Rewind_OnObstacleHit : AbilityBase
    {
        [SerializeField] private Transform _cubeRootTransform = null;
        
        [SerializeField] private FSM_Cube _cubeFSM = null;

        [SerializeField] private ECubeState _postRewindState = ECubeState.Idle;
        
        [SerializeField] private CubeFailController _cubeFailController = null;

        [SerializeField] private RewindController _rewindController = null;
        
        [SerializeField] private KeyCode _rewindTriggerKeyCode = KeyCode.R;

        [SerializeField] private CubeFailController.ECubeFail[] _cubeFailTypes = default;

        public override void Attached()
        {
            RegisterToCubeFSM();

            RegisterToLevelFSM();

            RegisterToCubeFailController();

            ActivateRewindables();
        }

        public override void Detached()
        {
            UnregisterFromCubeFSM();

            UnregisterFromLevelFSM();

            UnregisterFromCubeFailController();
            
            UnregisterFromInputManager();

            UnregisterFromRewindController();
            
            DeactivateRewindables();
        }

        private void OnDestroy()
        {
            UnregisterFromCubeFSM();
            
            UnregisterFromLevelFSM();

            UnregisterFromCubeFailController();
            
            UnregisterFromInputManager();

            UnregisterFromRewindController();
            
            DeactivateRewindables();
        }

        private void RegisterToCubeFSM()
        {
            _cubeFSM.OnStateEntered += OnCubeStateEntered;
        }

        private void UnregisterFromCubeFSM()
        {
            _cubeFSM.OnStateEntered -= OnCubeStateEntered;
        }

        private void RegisterToLevelFSM()
        {
            Level.Level.Instance.LevelFSM.OnStateEntered += OnLevelStateEntered;
            Level.Level.Instance.LevelFSM.OnStateExited += OnLevelStateExited;
        }
        
        private void UnregisterFromLevelFSM()
        {
            Level.Level.Instance.LevelFSM.OnStateEntered -= OnLevelStateEntered;
            Level.Level.Instance.LevelFSM.OnStateExited -= OnLevelStateExited;
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
            IRewindable[] rewindables = _cubeRootTransform.GetComponentsInChildren<IRewindable>();

            foreach (IRewindable rewindable in rewindables)
            {
                rewindable.ActivateRewindable(_rewindController);
            }
        }

        private void DeactivateRewindables()
        {
            IRewindable[] rewindables = _cubeRootTransform.GetComponentsInChildren<IRewindable>();

            foreach (IRewindable rewindable in rewindables)
            {
                rewindable.DeactivateRewindable(_rewindController);
            }
        }
        
        private void OnLevelStateEntered(ELevelState levelState)
        {
            if (levelState.Equals(ELevelState.Platform))
            {
                _rewindController.StopSaveSnapshotRoutine();
                
                _rewindController.ResetSnapshots();
                
                _rewindController.StartSaveSnapshotRoutine();
            }
        }
        
        private void OnLevelStateExited(ELevelState levelState)
        {
            if (levelState.Equals(ELevelState.Platform))
            {
                _rewindController.StopSaveSnapshotRoutine();
                
                _rewindController.ResetSnapshots();
            }
        }
        
        private void OnCubeStateEntered(ECubeState cubeState)
        {
            if (cubeState == ECubeState.Rewind)
            {
                _rewindController.StopSaveSnapshotRoutine();
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