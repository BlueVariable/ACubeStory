using System;
using System.Collections;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class RollBehaviour_RollPointBased : RollBehaviourBase
    {
        [Header("Roll Settings")]
        [SerializeField] private Transform _rollableTransform = null;
        [SerializeField] private AnimationCurve _rollCurve = null;

        [Header("Providers")]
        [SerializeField] private Transform _rollPointProviderCarrier = null;
        [SerializeField] private Transform _speedProviderCarrier = null;
        
        private ISpeedProvider _speedProvider = null;
        private ISpeedProvider _SpeedProvider
        {
            get
            {
                if (_speedProvider == null)
                {
                    _speedProvider = _speedProviderCarrier.GetComponent<ISpeedProvider>();
                }

                return _speedProvider;
            }
        }
        
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

        private ERollDirection _direction;

        private Action _onRolledCallback;

        protected override void StartRoll(ERollDirection direction, Action onRolledCallback = null)
        {
            _onRolledCallback = onRolledCallback;
            
            _direction = direction;
            
            StartRollRoutine();
        }

        public override void StopRoll()
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

            float targetAngle = 90f;

            Vector3 rollPoint = _RollPointProvider.GetRollPoint(_direction);

            while (true)
            {
                float prevAngle = _rollCurve.Evaluate(curNormValue) * targetAngle;

                float step = Time.deltaTime * _SpeedProvider.GetSpeed();
   
                curNormValue += step;

                curNormValue = Mathf.Min(curNormValue, targetNormValue);
            
                float curAngle = _rollCurve.Evaluate(curNormValue) * targetAngle;
                
                _rollableTransform.RotateAround(rollPoint, GetRollAxis(_direction), curAngle - prevAngle);
            
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