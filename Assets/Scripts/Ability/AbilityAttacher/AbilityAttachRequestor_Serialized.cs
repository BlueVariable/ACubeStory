using UnityEngine;

namespace BLUE.ACubeStory.Ability
{
    public class AbilityAttachRequestor_Serialized : AbilityAttachRequestorBase
    {
        [SerializeField] private AbilityAttacher.EAbility _ability = AbilityAttacher.EAbility.None;
        public override AbilityAttacher.EAbility Ability => _ability;
    }
}