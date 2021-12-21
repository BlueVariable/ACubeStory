using UnityEngine;

namespace BLUE.ACubeStory.Ability
{
    public abstract class AbilityAttachRequestorBase : MonoBehaviour
    {
        public abstract AbilityAttacher.EAbility Ability { get; }
    }
}