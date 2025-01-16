using System;
using System.Collections;
using BLUE.ActionSystem;
using MegaFiers;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Action_BendCube : InterruptableActionBase
    {
        [SerializeField] private MegaBend _bend = null;

        [SerializeField] private float _duration = 0.2f;
        
        [SerializeField] private AnimationCurve _angleCurve = AnimationCurve.EaseInOut(0, -30, 1, 30);

        private IEnumerator _bendRoutine;
        
        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion

        public override void Execute()
        {
            Interrupt();

            StartBend();
        }
        
        public override void Interrupt()
        {
            StopBend();
        }

        private void OnDestroy()
        {
            Interrupt();
        }

        private void StartBend()
        {
            _bendRoutine = BendProgress();
            
            StartCoroutine(_bendRoutine);
        }

        private IEnumerator BendProgress()
        {
            float elapsedTime = 0f;

            while (elapsedTime < _duration)
            {
                float angle = _angleCurve.Evaluate(elapsedTime/_duration);
                
                _bend.angle = angle;
                
                elapsedTime += Time.deltaTime;
                
                yield return null;
            }

            OnCompleted?.Invoke(this);
        }

        private void StopBend()
        {
            if (_bendRoutine != null)
            {
                StopCoroutine(_bendRoutine);
                _bendRoutine = null;
            }
        }
    }
}
