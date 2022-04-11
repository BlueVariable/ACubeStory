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
        
        public Vector3 LastRollPoint { get; private set; }
        
        private IEnumerator _rollRoutine;

        private Vector3 _direction;

        private float _angle;

        private float _rollSpeed;
        
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
            _rollSpeed = rollSpeed;
            _rollCurve = rollCurve ?? _defaultRollCurve;
            _onRolledCallback = onRolledCallback;
            
            LastRollPoint = _RollPointProvider.GetRollPoint(_direction) + rollPointOffset;

            StartRollRoutine();
        }

        protected override void PauseRoll()
        {
            StopRollRoutine();
        }

        protected override void UndoRoll(Action onRolledCallback = null, float undoSpeedMultiplier = 1)
        {
            StopRollRoutine();
            
            _direction *= -1;
            _angle = CurRollAngle;
            _rollSpeed *= undoSpeedMultiplier;
            _onRolledCallback = onRolledCallback;
            
            StartRollRoutine();
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
            
            while (true)
            {
                float prevAngle = _rollCurve.Evaluate(curNormValue) * targetAngle;

                float step = Time.deltaTime * _rollSpeed;
   
                curNormValue += step;

                curNormValue = Mathf.Min(curNormValue, targetNormValue);
            
                CurRollAngle = _rollCurve.Evaluate(curNormValue) * targetAngle;
                
                _rollableTransform.RotateAround(LastRollPoint, GetRollAxis(_direction), CurRollAngle - prevAngle);

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