using System;
using BLUE.ActionSystem;
using BLUE.Common;
using MegaFiers;
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
        }
        
        public override void Interrupt()
        {
            EDirection direction = _stretchData.TargetCubeFaceDirectionProvider.GetDirection();

            if (!_stretchData.ModifierProvider.TryGetModifier(direction, out MegaModifier modifier))
            {
                Debug.LogError("Action_StretchCube => No modifier found for direction: " + direction + ".");
                
                return;
            }
            
            _stretchController.StopStretchRoutine(modifier);
        }

        private void OnDestroy()
        {
            Interrupt();
        }
    }
}
