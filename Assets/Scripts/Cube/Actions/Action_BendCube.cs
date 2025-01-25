using System;
using BLUE.ActionSystem;
using BLUE.Common;
using MegaFiers;
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
            
            _bendController.StartBend(_bendData);
        }
        
        public override void Interrupt()
        {
            EDirection direction = _bendData.TargetCubeFaceDirectionProvider.GetDirection();

            if (!_bendData.ModifierProvider.TryGetModifier(direction, out MegaModifier modifier))
            {
                Debug.Log("Action_BendCube: No modifier found for direction: " + direction);
                return;
            }
            
            _bendController.StopBend(modifier);
        }

        private void OnDestroy()
        {
            Interrupt();
        }
    }
}
