using BLUE.AttachableSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Behaviours
{
    public abstract class RollPointProviderBase : MonoBehaviour, IAttachable, IRollPointProvider
    {
        [SerializeField] private bool _isInitiallyAttached = false;

        public bool IsAttachedInitially => _isInitiallyAttached;
        
        public abstract Vector3 GetRollPoint(RollBehaviourBase.ERollDirection direction);

        public void Attached(){}

        public void Detached(){}
    }
}