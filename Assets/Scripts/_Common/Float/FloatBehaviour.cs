using System;
using System.Collections;
using BLUE.Common;
using BLUE.Extensions;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class FloatBehaviour : ActivatableBehaviourBase
    {
        [SerializeField] private Transform _floatedTransform = null;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _lowestOffset = 0.2f;
        [SerializeField] private float _highestOffset = 0.5f; 

        private IEnumerator _floatRoutine;
        
        public override void Activate()
        {
            StartFloatRoutine();
        }

        public override void Deactivate()
        {
            StopFloatRoutine();
        }

        private void StartFloatRoutine()
        {
            StopFloatRoutine();

            _floatRoutine = FloatProgress();

            StartCoroutine(_floatRoutine);
        }

        private void StopFloatRoutine()
        {
            if (_floatRoutine != null)
            {
                StopCoroutine(_floatRoutine);
            }
        }

        private IEnumerator FloatProgress()
        {
            while (true)
            {
                yield return MoveOffsetProgress(_highestOffset);
                
                yield return MoveOffsetProgress(_lowestOffset);
            }
        }

        private IEnumerator MoveOffsetProgress(float offset)
        {
            Vector3 targetPosition;
            
            do
            {
                targetPosition = offset * _floatedTransform.InverseTransformDirection(Vector3.up);

                _floatedTransform.localPosition = Vector3.Lerp(_floatedTransform.localPosition,targetPosition, Time.deltaTime * _speed);
                
                yield return null;
            } while (Vector3.Distance(_floatedTransform.localPosition, targetPosition) > 0.1f);
        }
    }
}