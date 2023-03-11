using System;
using BLUE.ActionSystem;
using BLUE.ACubeStory.Ability;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Action_DetachCubeAbilities : ActionBase
    {
        [SerializeField] private ECubeAbility[] _targetAbilities = default;

        [SerializeField] private AbilityAttachableControllerProvider_Cube _abilityAttachableControllerProvider = null;

        [SerializeField] private AbilityProvider_Cube _cubeAbilityProvider = null;

        public override void Execute()
        {
            AttachableController_Ability _abilityAttachableController = _abilityAttachableControllerProvider.GetAbilityAttachableController();
            
            _cubeAbilityProvider.SetProvidedAbilityTypes(_targetAbilities);
            
            Array.ForEach(_cubeAbilityProvider.GetAbilities(), val => _abilityAttachableController.TryDetach(val));
        }
    }
}