using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube.Conditions
{
    public class StateEnterCondition_IsPreDashKeyActive : StateEnterConditionBase
    {
        [SerializeField] private Ability_Cube_Dash _cubeDashAbility = null;

        [SerializeField] private bool _negateResult = false;
        
        public override bool CanStateBeEntered()
        {
            if (_negateResult)
            {
                return !_cubeDashAbility.IsPreDashKeyActive;
            }
            
            return _cubeDashAbility.IsPreDashKeyActive;
        }
    }
}