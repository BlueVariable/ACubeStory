using UnityEngine;

namespace BLUE.ACubeStory.Ability
{
    public class AbilityAttachRequester_Serialized : AbilityAttachRequesterBase
    {
        [SerializeField] private AbilityAttacher.EAbility _ability = AbilityAttacher.EAbility.None;

        public override AbilityAttacher.EAbility Ability => _ability;
    }
}