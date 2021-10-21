using BLUE.AttachableSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Ability
{
    public abstract class AbilityBase : MonoBehaviour, IAttachable
    {
        [SerializeField] private bool _isInitiallyAttached = false;
        
        public bool IsAttachedInitially
        {
            get => _isInitiallyAttached;
        }

        public abstract void Attached();

        public abstract void Detached();
    }
}