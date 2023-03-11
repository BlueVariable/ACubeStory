using System;
using System.Collections.Generic;
using BLUE.ACubeStory.Ability;

namespace BLUE.ACubeStory.Cube
{
    public class AbilityTypeInfo_Cube : AbilityTypeInfoBase<ECubeAbility>
    {
        private readonly Dictionary<ECubeAbility, Type> _cubeAbilityTypeMapping = new()
        {
            {ECubeAbility.Roll_2D, typeof(Ability_Cube_Roll_2D)},
            {ECubeAbility.Roll_3D, typeof(Ability_Cube_Roll_3D)},
            {ECubeAbility.Dash, typeof(Ability_Cube_Dash)},
            {ECubeAbility.Jump, typeof(Ability_Cube_Jump)},
            {ECubeAbility.Float, typeof(Ability_Cube_Float)},
            {ECubeAbility.Rewind, typeof(Ability_Cube_Rewind)},
        };

        public override Dictionary<ECubeAbility, Type> GetAbilityTypeInfoMapping()
        {
            return _cubeAbilityTypeMapping;
        }
    }
}