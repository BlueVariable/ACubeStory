using MegaFiers;
using UnityEngine;

namespace BLUE.ACubeStory.MegaFiers
{
    public abstract class MegaModifierProviderBase : MonoBehaviour
    {
        public abstract MegaModifier GetModifier();
    }
}