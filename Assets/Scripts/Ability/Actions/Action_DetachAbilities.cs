using System;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Ability
{
    public class Action_DetachAbilities : ActionBase
    {
        [SerializeField] private Transform _abilityAttachableControllerProviderObj = null;

        [SerializeField] private Transform _abilityProviderObj = null;
        
        private IAbilityAttachableControllerProvider _abilityAttachableControllerProvider;
        private IAbilityAttachableControllerProvider _AbilityAttachableControllerProvider
        {
            get
            {
                if (_abilityAttachableControllerProvider == null)
                {
                    _abilityAttachableControllerProvider = _abilityAttachableControllerProviderObj
                        .GetComponent<IAbilityAttachableControllerProvider>();
                }

                return _abilityAttachableControllerProvider;
            }
        }
        
        private IAbilityProvider _abilityProvider;
        private IAbilityProvider _AbilityProvider
        {
            get
            {
                if (_abilityProvider == null)
                {
                    _abilityProvider = _abilityProviderObj
                        .GetComponent<IAbilityProvider>();
                }

                return _abilityProvider;
            }
        }
        
        public override void Execute()
        {
            AttachableController_Ability _abilityAttachableController = _AbilityAttachableControllerProvider.GetAbilityAttachableController();
            
            Array.ForEach(_AbilityProvider.GetAbilities(), val => _abilityAttachableController.TryDetach(val));
        }
    }
}