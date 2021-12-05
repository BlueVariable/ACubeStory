using BLUE.ACubeStory.Ability;
using BLUE.ACubeStory.RewindSystem;
using BLUE.InputSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube.Ability
{
    public class Ability_Cube_Rewind_Fall : AbilityBase
    {
        [SerializeField] private Transform _cubeRootTransform = null;
        
        [SerializeField] private FSM_Cube _cubeFSM = null;

        [SerializeField] private RewindController _rewindController = null;
        
        [SerializeField] private KeyCode _rewindTriggerKeyCode = KeyCode.R;
        
        public override void Attached()
        {
            RegisterToCubeFSM();

            ActivateRewindables();
        }

        public override void Detached()
        {
            UnregisterFromCubeFSM();
            
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
        
        private void OnCubeStateEntered(ECubeState cubeState)
        {
            if (cubeState == ECubeState.Idle)
            {
                _rewindController.StopSaveSnapshotRoutine();
                
                _rewindController.ResetSnapShots();
                
                _rewindController.StartSaveSnapshotRoutine();
            }
            
            if (cubeState == ECubeState.Fall)
            {
                RegisterToInputManager();
            }
        }
        
        private void OnCubeStateExited(ECubeState cubeState)
        {
            if (cubeState == ECubeState.Fall)
            {
                UnregisterFromInputManager();
            }
        }

        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(EInputEvent.KeyDown, OnKeyDown);
        }

        private void UnregisterFromInputManager()
        {
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(EInputEvent.KeyDown, OnKeyDown);
        }

        private void OnKeyDown(InputData_Key inputData)
        {
            if (inputData.KeyCode == _rewindTriggerKeyCode)
            {
                // Start Rewind here
            }
        }
    }
}