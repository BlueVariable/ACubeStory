using System.Collections;
using System.Collections.Generic;
using BLUE.ACubeStory.Ability;
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

        private List<RewindSnapShot> _savedSnapShots = new List<RewindSnapShot>(); 

        private IEnumerator _saveSnapShotRoutine;

        public override void Attached()
        {
            _cubeFSM.OnStateEntered += OnCubeStateEntered;

            SaveSnapShot();
        }

        public override void Detached()
        {
            _cubeFSM.OnStateEntered -= OnCubeStateEntered;
        }

        private void OnCubeStateEntered(ECubeState cubeState)
        {
            if (cubeState == ECubeState.Idle)
            {
                StopSnapShotSaveRoutine();
                
                ResetSavedSnapShots();
                
                StartSnapShotSaveRoutine();
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
    }
}