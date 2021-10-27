using BLUE.AttachableSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class AttachableController_SpeedProvider : 
        AttachableControllerBase<SpeedProviderBase>,
        ISpeedProvider
    {
        [SerializeField] private float _fallbackSpeed = 0;
        
        public float GetSpeed()
        {
            if (TryGetSingleAttachable(out SpeedProviderBase speedProvider))
            {
                return speedProvider.GetSpeed();
            }

            return _fallbackSpeed;
        }
    }
}