using System;
using BLUE.ActionSystem;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Action_StretchCube : InterruptableActionBase
    {
        [SerializeField] private CubeStretchController _stretchController = null;
        
        [SerializeField] private CubeStretchController.StretchData _stretchData = null;
        
        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion

        public override void Execute()
        {
            Interrupt();
            
            _stretchController.StartStretchRoutine(_stretchData);
            
            OnCompleted?.Invoke(this);
        }
        
        public override void Interrupt()
        {
            EDirection direction = _stretchData.TargetCubeFaceDirectionProvider.GetDirection();
            
            _stretchController.StopStretchRoutine(direction);
        }

        private void OnDestroy()
        {
            Interrupt();
        }
    }
}
