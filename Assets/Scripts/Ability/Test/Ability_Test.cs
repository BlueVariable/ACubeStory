using System.Collections;
using UnityEngine;

namespace BLUE.ACubeStory.Ability
{
    public class Ability_Test : AbilityBase
    {
        [SerializeField] private Transform _movable = null;
        
        [SerializeField] private Vector3 _moveOffset = Vector3.one;

        private IEnumerator _moveRoutine;
        
        public override void Attached()
        {
            StartMoveRoutine();
        }

        public override void Detached()
        {
            StopMoveRoutine();
        }

        private void OnDestroy()
        {
            StopMoveRoutine();
        }

        private void StartMoveRoutine()
        {
            StopMoveRoutine();

            _moveRoutine = MoveProgress();

            StartCoroutine(_moveRoutine);
        }

        private void StopMoveRoutine()
        {
            if (_moveRoutine != null)
            {
                StopCoroutine(_moveRoutine);
            }
        }

        private IEnumerator MoveProgress()
        {
            while (true)
            {
                _movable.position += _moveOffset;

                yield return null;
            }
        }
    }
}