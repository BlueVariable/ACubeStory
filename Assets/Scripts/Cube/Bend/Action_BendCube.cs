using System;
using BLUE.ActionSystem;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Action_BendCube : InterruptableActionBase
    {
        [SerializeField] private CubeBendController _bendController = null;
        
        [SerializeField] private CubeBendController.BendData _bendData = null;
        
        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion

        public override void Execute()
        {
            Interrupt();
            
            _bendController.StartBendRoutine(_bendData);
            
            OnCompleted?.Invoke(this);
        }
        
        public override void Interrupt()
        {
            EDirection direction = _bendData.TargetCubeFaceDirectionProvider.GetDirection();

            _bendController.StopBendRoutine(direction);
        }

        private void OnDestroy()
        {
            Interrupt();
        }
    }
}
