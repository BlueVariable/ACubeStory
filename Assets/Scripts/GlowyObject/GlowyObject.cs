using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.GlowyObject
{
    public class GlowyObject : MonoBehaviour
    {
        [SerializeField] private TriggerBase _glowTrigger = null;
        public TriggerBase GlowTrigger => _glowTrigger;
    }
}