using System;
using System.Collections;
using System.Collections.Generic;
using BLUE.ActionSystem;
using BLUE.ACubeStory.MegaFiers;
using BLUE.Common;
using MegaFiers;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Action_BendCube : InterruptableActionBase
    {
        [SerializeField] private float _duration = 0.2f;
        
        [SerializeField] private AnimationCurve _curve = AnimationCurve.EaseInOut(0, -30, 1, 30);
        
        [SerializeField] private float _multiplier = 1f;

        [SerializeField] private MegaModifierProviderBase _modifierProvider = null;
        
        [SerializeField] private DirectionProviderBase _bendDirectionProvider = null;

        private IEnumerator _warpRoutine;

        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion

        private MegaBend _curModifier;

        public override void Execute()
        {
            Interrupt();

            StartWarp();
        }
        
        public override void Interrupt()
        {
            StopWarp();
        }

        private void OnDestroy()
        {
            Interrupt();
        }

        private void StartWarp()
        {
            _warpRoutine = WarpProgress();
            
            StartCoroutine(_warpRoutine);
        }

        private IEnumerator WarpProgress()
        {
            //TODO
            
            _curModifier = (MegaBend)_modifierProvider.GetModifier();

            EDirection bendDirection = _bendDirectionProvider.GetDirection();

            Vector3 bendVector = DirectionUtility.DirectionMap[bendDirection];

            Vector3 cubeComparisonVector = _curModifier.axis == MegaAxis.X || _curModifier.axis == MegaAxis.Z
                ? Cube.Instance.transform.forward
                : Cube.Instance.transform.up;
            
            float dot = Vector3.Dot(cubeComparisonVector, bendVector);
            

            if (dot > 0.9f)
            { 
                _curModifier.dir = 90;
            }
            else if (dot < -0.9f)
            {
                _curModifier.dir = -90;
            }
            else
            { 
                if (_curModifier.axis == MegaAxis.X)
                {
                    float signedAngle = Vector3.SignedAngle(cubeComparisonVector, bendVector, Cube.Instance.transform.up);
                
                    _curModifier.dir = signedAngle > 0 ? 0 : 180;
                }
                else if(_curModifier.axis == MegaAxis.Y)
                {
                    float signedAngle = Vector3.SignedAngle(cubeComparisonVector, bendVector, Cube.Instance.transform.right);
                
                    _curModifier.dir = signedAngle > 0 ? 180 : 0;
                }
                else if(_curModifier.axis == MegaAxis.Z)
                {
                    float signedAngle = Vector3.SignedAngle(cubeComparisonVector, bendVector, Cube.Instance.transform.up);
                
                    _curModifier.dir = signedAngle > 0 ? 180 : 0;
                }

            }
            
            float elapsedTime = 0f;

            while (elapsedTime < _duration)
            {
                float value = _curve.Evaluate(elapsedTime/_duration) * _multiplier;
                
                _curModifier.angle = value;
                
                elapsedTime += Time.deltaTime;
                
                yield return null;
            }
            
            float finalValue = _curve.Evaluate(1) * _multiplier;

            _curModifier.angle = finalValue;

            OnCompleted?.Invoke(this);
        }

        private void StopWarp()
        {
            if (_warpRoutine != null)
            {
                StopCoroutine(_warpRoutine);
                
                float finalValue = _curve.Evaluate(1) * _multiplier;

                _curModifier.angle = finalValue;

                _warpRoutine = null;
            }
        }
    }
}
