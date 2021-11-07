using System;
using System.Collections;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class RollBehaviour_RollPointBased : RollBehaviourBase
    {
        [Header("Roll Settings")]
        [SerializeField] private Transform _rollableTransform = null;
        [SerializeField] private AnimationCurve _defaultRollCurve = null;

        [Header("Providers")]
        [SerializeField] private Transform _rollPointProviderCarrier = null;

        private IRollPointProvider _rollPointProvider = null;
        private IRollPointProvider _RollPointProvider
        {
            get
            {
                if (_rollPointProvider == null)
                {
                    _rollPointProvider = _rollPointProviderCarrier.GetComponent<IRollPointProvider>();
                }

                return _rollPointProvider;
            }
        }
        
        private IEnumerator _rollRoutine;

        private Vector3 _direction;

        private float _angle;

        private float _rollSpeed;
        
        private Vector3 _rollPointOffset;

        private AnimationCurve _rollCurve;

        private Action _onRolledCallback;

        protected override void StartRoll(
            Vector3 direction, 
            float angle = 90f,
            float rollSpeed = 5f,
            Vector3 rollPointOffset = default,
            AnimationCurve rollCurve = default,
            Action onRolledCallback = null)
        {
            _direction = direction;
            _angle = angle;
            _rollPointOffset = rollPointOffset;
            _rollSpeed = rollSpeed;
            _rollCurve = rollCurve ?? _defaultRollCurve;
            _onRolledCallback = onRolledCallback;
            
            StartRollRoutine();
        }

        protected override void PauseRoll()
        {
            StopRollRoutine();
        }

        private void StartRollRoutine()
        {
            StopRollRoutine();

            _rollRoutine = RollProgress();

            StartCoroutine(_rollRoutine);
        }

        private void StopRollRoutine()
        {
            if (_rollRoutine != null)
            {
                StopCoroutine(_rollRoutine);
            }
        }
        
        private IEnumerator RollProgress()
        {
            float curNormValue = 0f;

            float targetNormValue = 1f;

            float targetAngle = _angle;

            Vector3 rollPoint = _RollPointProvider.GetRollPoint(_direction) + _rollPointOffset;

            while (true)
            {
                float prevAngle = _rollCurve.Evaluate(curNormValue) * targetAngle;

                float step = Time.deltaTime * _rollSpeed;
   
                curNormValue += step;

                curNormValue = Mathf.Min(curNormValue, targetNormValue);
            
                CurRollAngle = _rollCurve.Evaluate(curNormValue) * targetAngle;
                
                _rollableTransform.RotateAround(rollPoint, GetRollAxis(_direction), CurRollAngle - prevAngle);
            
                if (curNormValue >= targetNormValue)
                {
                    break;
                }
            
                yield return new WaitForSeconds(Time.deltaTime);
            }
            
            _onRolledCallback?.Invoke();
        }
    }
}