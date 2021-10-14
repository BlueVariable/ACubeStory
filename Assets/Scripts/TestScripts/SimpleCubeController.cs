using System;
using System.Collections;
using UnityEngine;

namespace TestScripts
{
    public class SimpleCubeController : MonoBehaviour
    {
        [Serializable]
        public class KeyCodeRotationMapSetting
        {
            [SerializeField] private KeyCode _keyCode = default;
            public KeyCode KeyCode => _keyCode;

            [SerializeField] private Vector3 _rotationPoint = default;
            public Vector3 RotationPoint => _rotationPoint;

            [SerializeField] private Vector3 _axis = default;
            public Vector3 Axis => _axis;
        }
    
        [SerializeField] private float _rotationSpeed = 100f;

        [SerializeField] private KeyCodeRotationMapSetting[] _keyCodeRotationMapSettings = new KeyCodeRotationMapSetting[0];

        [SerializeField] private AnimationCurve _movementCurve = null;
    
        private IEnumerator _processInputRoutine;
    
        private void Start()
        {
            StartProcessInputRoutine();
        }

        private void StartProcessInputRoutine()
        {
            StopProcessInputRoutine();

            _processInputRoutine = ProcessInput();

            StartCoroutine(_processInputRoutine);
        }

        private void StopProcessInputRoutine()
        {
            if (_processInputRoutine != null)
            {
                StopCoroutine(_processInputRoutine);
            }
        }

        private IEnumerator ProcessInput()
        {
            while (true)
            {
                foreach (KeyCodeRotationMapSetting setting in _keyCodeRotationMapSettings)
                {
                    if (Input.GetKey(setting.KeyCode))
                    {
                        yield return Rotate(setting);
                    }
                }

                yield return null;
            }
        }

        private IEnumerator Rotate(KeyCodeRotationMapSetting setting)
        {
            float target = 0;

            Vector3 rotationPoint = transform.position + setting.RotationPoint;

            while (true)
            {
                float prevAngle = _movementCurve.Evaluate(target) * 90f;

                float step = Time.deltaTime * _rotationSpeed;
   
                target += step;

                target = Mathf.Min(target, 1f);
            
                float curAngle = _movementCurve.Evaluate(target) * 90f;
                
                transform.RotateAround(rotationPoint, setting.Axis, curAngle - prevAngle);
            
                if (target >= 1)
                {
                    break;
                }
            
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
    }
}
