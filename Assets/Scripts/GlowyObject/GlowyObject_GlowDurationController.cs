using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.GlowyObject
{
    public class GlowyObject_GlowDurationController : MonoBehaviour
    {
        [SerializeField] private Action_Wait _waitAction = null;
        
        public bool TrySetDuration(float duration)
        {
            return _waitAction.TrySetDuration(duration);
        }
    }
}
