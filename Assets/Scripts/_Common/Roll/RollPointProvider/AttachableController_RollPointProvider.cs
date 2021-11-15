using System;
using BLUE.AttachableSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class AttachableController_RollPointProvider : 
        AttachableControllerBase<RollPointProviderBase>,
        IRollPointProvider
    {
        [SerializeField] private Vector3 _fallbackRollPoint = Vector3.zero;
        
        public Vector3 GetRollPoint(Vector3 direction, Action<Vector3> onRollPointUpdatedCallback = null)
        {
            if (TryGetSingleAttachable(out RollPointProviderBase rollPointProvider))
            {
                return rollPointProvider.GetRollPoint(direction, onRollPointUpdatedCallback);
            }

            return _fallbackRollPoint;
        }
    }
}