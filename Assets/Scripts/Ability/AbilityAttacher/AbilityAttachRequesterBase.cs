using UnityEngine;

namespace BLUE.ACubeStory.Ability
{
    public abstract class AbilityAttachRequesterBase : MonoBehaviour
    {
        public abstract AbilityAttacher.EAbility Ability { get; }
    }
}