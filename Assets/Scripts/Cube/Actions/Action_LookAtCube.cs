using System;
using BLUE.ActionSystem;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Action_LookAtCube : InterruptableActionBase
    {
        [SerializeField] private RotationBehaviour_LerpAngle _rotationBehaviour = null;

        [SerializeField] private RotationBehaviour_LerpAngle.RotationSettings _rotationSettings = null;
        
        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion

        public override void Execute()
        {
            Interrupt();

            StartRotation();
        }
        
        public override void Interrupt()
        {
            StopRotation();
        }

        private void OnDestroy()
        {
            Interrupt();
        }

        private void StartRotation()
        { 
            void onRotated(bool isSatisfied)
            {
                OnCompleted?.Invoke(this);
            }
            
            _rotationBehaviour.StartRotation(
                _rotationSettings,
                Cube.Instance.transform,
                onRotated);
        }

        private void StopRotation()
        {
            if (_rotationBehaviour == null)
            {
                return;
            }
            
            _rotationBehaviour.StopRotation();
        }
    }
}
