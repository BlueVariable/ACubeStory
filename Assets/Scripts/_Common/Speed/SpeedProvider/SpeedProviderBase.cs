using BLUE.AttachableSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public abstract class SpeedProviderBase : MonoBehaviour, IAttachable, ISpeedProvider
    {
        [SerializeField] private bool _isInitiallyAttached = false;

        public bool IsAttachedInitially => _isInitiallyAttached;
        
        public abstract float GetSpeed();

        public void Attached(){}

        public void Detached(){}
    }
}