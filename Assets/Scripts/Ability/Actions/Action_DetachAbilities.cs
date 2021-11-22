using System;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Ability
{
    public class Action_DetachAbilities : ActionBase
    {
        [SerializeField] private AttachableController_Ability _abilityAttachableController = null;

        [SerializeField] private AbilityBase[] _abilitiesToDetach = default;
        
        public override void Execute()
        {
            Array.ForEach(_abilitiesToDetach, val => _abilityAttachableController.TryDetach(val));
        }
    }
}