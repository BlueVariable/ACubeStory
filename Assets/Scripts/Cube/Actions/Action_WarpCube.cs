using System;
using System.Collections;
using System.Collections.Generic;
using BLUE.ActionSystem;
using BLUE.ACubeStory.MegaFiers;
using MegaFiers;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Action_WarpCube : InterruptableActionBase
    {
        [SerializeField] private float _duration = 0.2f;
        
        [SerializeField] private AnimationCurve _curve = AnimationCurve.EaseInOut(0, -30, 1, 30);
        
        [SerializeField] private float _multiplier = 1f;

        [SerializeField] private MegaModifierProviderBase _modifierProvider = null;
        
        private IEnumerator _warpRoutine;

        private delegate void _warpModifierDel(MegaModifier modifier, float value);

        private Dictionary<Type, _warpModifierDel> _warpModifierDelMap = new()
        {
            { typeof(MegaBend), (modifier, value) => { ((MegaBend)modifier).angle = value; } },
        };
        
        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion

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
            
            MegaModifier modifier = _modifierProvider.GetModifier();
            
            float elapsedTime = 0f;

            while (elapsedTime < _duration)
            {
                float value = _curve.Evaluate(elapsedTime/_duration) * _multiplier;
                
                _warpModifierDelMap[modifier.GetType()].Invoke(modifier, value);
                
                elapsedTime += Time.deltaTime;
                
                yield return null;
            }

            OnCompleted?.Invoke(this);
        }

        private void StopWarp()
        {
            if (_warpRoutine != null)
            {
                StopCoroutine(_warpRoutine);
                _warpRoutine = null;
            }
        }
    }
}
