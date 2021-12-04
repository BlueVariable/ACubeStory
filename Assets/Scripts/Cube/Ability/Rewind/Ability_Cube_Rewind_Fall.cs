using System.Collections;
using System.Collections.Generic;
using BLUE.ACubeStory.Ability;
using BLUE.InputSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube.Ability
{
    public class RewindSnapShot
    {
        public RewindSnapShot(Vector3 position, Quaternion rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        public Vector3 Position { get; }

        public Quaternion Rotation { get; }

        public override bool Equals(object other)
        {
            if (other == null || !(other is RewindSnapShot))
            {
                return false;
            }

            RewindSnapShot otherSnapShot = (RewindSnapShot) other;

            return Position == otherSnapShot.Position &&
                   Rotation == otherSnapShot.Rotation;
        }
    }

    public class Ability_Cube_Rewind_Fall : AbilityBase
    {
        [SerializeField] private Transform _cubeRootTransform;

        [SerializeField] private FSM_Cube _cubeFSM;

        [SerializeField] private KeyCode _rewindTriggerKeyCode = KeyCode.R;
        
        private List<RewindSnapShot> _savedSnapShots = new List<RewindSnapShot>(); 

        private IEnumerator _saveSnapShotRoutine;

        private IEnumerator _rewindRoutine;

        public override void Attached()
        {

            SaveSnapShot();
        }

        public override void Detached()
        {
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

        private void OnCubeStateEntered(ECubeState cubeState)
        {
            if (cubeState == ECubeState.Idle)
            {
                StopSnapShotSaveRoutine();
                
                ResetSavedSnapShots();
                
                StartSnapShotSaveRoutine();
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
                
            }
        }

        private void SaveSnapShot()
        {
            _savedSnapShots.Add(new RewindSnapShot(_cubeRootTransform.position, _cubeRootTransform.rotation));
        }

        private void ResetSavedSnapShots()
        {
            _savedSnapShots.Clear();
        }
        
        private void StartSnapShotSaveRoutine()
        {
            StopSnapShotSaveRoutine();

            _saveSnapShotRoutine = SnapShotSaveRoutine();

            StartCoroutine(_saveSnapShotRoutine);
        }

        private void StopSnapShotSaveRoutine()
        {
            if (_saveSnapShotRoutine != null)
            {
                StopCoroutine(_saveSnapShotRoutine);
            }
        }

        private IEnumerator SnapShotSaveRoutine()
        {
            while (true)
            {
                SaveSnapShot();
                
                yield return null;
            }
        }

        private void StartRewindRoutine()
        {
            StopRewindRoutine();

            _rewindRoutine = RewindProgress();

            StartCoroutine(_rewindRoutine);
        }

        private void StopRewindRoutine()
        {
            if (_rewindRoutine != null)
            {
                StopCoroutine(_rewindRoutine);
            }
        }

        private IEnumerator RewindProgress()
        {
            for (int i = _savedSnapShots.Count - 1; i > 0; i--)
            {
                _cubeRootTransform.position = _savedSnapShots[i].Position;
                _cubeRootTransform.rotation = _savedSnapShots[i].Rotation;
                
                yield return null;
            }
        }
    }
}