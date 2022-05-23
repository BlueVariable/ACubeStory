using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.GlowyObject
{
    public class GlowyObject : MonoBehaviour
    {
        [SerializeField] private TriggerBase[] _glowTriggers = null;
        public TriggerBase[] GlowTriggers => _glowTriggers;
    }
}