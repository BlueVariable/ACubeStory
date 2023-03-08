using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class AbilityAttachRequestor_Cube_Serialized : AbilityAttachRequestor_CubeBase
    {
        [SerializeField] private ECubeAbility _abilityType = ECubeAbility.None;

        public override ECubeAbility AbilityType => _abilityType;
    }
}