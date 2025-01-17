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
        
        private Dictionary<EDirection, float> _directionMap = new Dictionary<EDirection, float>
        {
            { EDirection.Up, 0 },
            { EDirection.Down, 180 },
            { EDirection.Left, 90 },
            { EDirection.Right, -90 },
            { EDirection.Forward, 0 },
            { EDirection.Backward, 180 }
        };
        
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

            _curModifier.dir = _directionMap[_bendDirectionProvider.GetDirection()];
            
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
