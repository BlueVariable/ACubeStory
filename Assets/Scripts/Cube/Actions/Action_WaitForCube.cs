using System;
using System.Collections;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Action_WaitForCube : InterruptableActionBase
    {
        [Tooltip("Wait For Approach: True => Completion occurs when distance is less or equal." +
                 "Wait For Approach: False => Completion occurs when distance is greater.")]
        [SerializeField] private bool _waitForApproach = true;

        [SerializeField] private Transform _selfObject = null;
        
        [SerializeField] private float _targetDistance = 5f;

        private IEnumerator _waitRoutine;
        
        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion

        public override void Execute()
        {
            Interrupt();

            StartWaitRoutine();
        }
        
        public override void Interrupt()
        {
            StopWaitRoutine();
        }

        private void OnDestroy()
        {
            Interrupt();
        }

        private void StartWaitRoutine()
        {
            StopWaitRoutine();

            _waitRoutine = WaitProgress();

            StartCoroutine(_waitRoutine);
        }

        private void StopWaitRoutine()
        {
            if (_waitRoutine != null)
            {
                StopCoroutine(_waitRoutine);
            }
        }

        private IEnumerator WaitProgress()
        {
            while (true)
            {
                float distance = Vector2.Distance(
                    new Vector2(_selfObject.position.x, _selfObject.position.z),
                    new Vector2(Cube.Instance.GetPosition().x, Cube.Instance.GetPosition().z));
                
                if ((_waitForApproach && distance <= _targetDistance)
                    || (!_waitForApproach && distance > _targetDistance))
                {
                    OnCompleted?.Invoke(this);
                    
                    break;
                }

                yield return null;
            }
        }
    }
}
