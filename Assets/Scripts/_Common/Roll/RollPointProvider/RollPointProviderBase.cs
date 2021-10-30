using BLUE.AttachableSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public abstract class RollPointProviderBase : MonoBehaviour, IAttachable, IRollPointProvider
    {
        [SerializeField] private bool _isInitiallyAttached = false;

        public bool IsAttachedInitially => _isInitiallyAttached;
        
        public abstract Vector3 GetRollPoint(Vector3 direction);

        public void Attached(){}

        public void Detached(){}
    }
}