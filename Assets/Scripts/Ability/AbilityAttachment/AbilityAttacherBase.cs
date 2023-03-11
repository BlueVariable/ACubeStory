using System;
using System.Linq;
using BLUE.ActionSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Ability
{
    public abstract class AbilityAttacherBase<T> : MonoBehaviour
        where T : Enum
    {
        [SerializeField] private AttachableController_Ability _abilityAttachableController = null;
        
        [SerializeField] private Trigger_LayerBase _abilityAttachRequesterTrigger = null;

        [SerializeField] private Transform _abilityTypeInfoObj = null;
        
        [SerializeField] private Transform _abilityProviderObj = null;

        [SerializeField] private ActionExecutor _abilityAttachedActions = null;
        
        [SerializeField] private ActionExecutor _abilityDetachedActions = null;
        
        public T LastAttachedAbility { get; private set; }
        public T LastDetachedAbility { get; private set; }
        
        private AbilityTypeInfoBase<T> _abilityTypeInfo;
        private AbilityTypeInfoBase<T> _AbilityTypeInfo
        {
            get
            {
                if (_abilityTypeInfo == null)
                {
                    _abilityTypeInfo = _abilityTypeInfoObj.GetComponent<AbilityTypeInfoBase<T>>();
                }

                return _abilityTypeInfo;
            }
        }
        
        private IAbilityProvider _abilityProvider;
        private IAbilityProvider _AbilityProvider
        {
            get
            {
                if (_abilityProvider == null)
                {
                    _abilityProvider = _abilityProviderObj.GetComponent<IAbilityProvider>();
                }

                return _abilityProvider;
            }
        }

        public bool TryAttachAbility(T abilityType)
        {
            Type componentType = _AbilityTypeInfo.GetAbilityTypeInfoMapping().FirstOrDefault(
                i => i.Key.Equals(abilityType)).Value;
            
            AbilityBase[] abilities = _AbilityProvider.GetAbilities();

            AbilityBase foundAbility = abilities.FirstOrDefault(val => val.GetType() == componentType);
            
            if (!_abilityAttachableController.TryAttach(foundAbility))
            {
                return false;
            }

            LastAttachedAbility = abilityType;
            
            _abilityAttachedActions.Execute();

            return true;
        }
        
        public bool TryDetachAbility(T abilityType)
        {
            Type componentType = _AbilityTypeInfo.GetAbilityTypeInfoMapping().FirstOrDefault(
                i => i.Key.Equals(abilityType)).Value;
            
            AbilityBase[] abilities = _AbilityProvider.GetAbilities();

            AbilityBase foundAbility = abilities.FirstOrDefault(val => val.GetType() == componentType);
            
            if (!_abilityAttachableController.TryDetach(foundAbility))
            {
                return false;
            }
            
            LastDetachedAbility = abilityType;
            
            _abilityDetachedActions.Execute();

            return true;
        }

        private void Awake()
        {
            RegisterToTrigger();
        }

        private void OnDestroy()
        {
            UnregisterFromTrigger();
        }

        private void RegisterToTrigger()
        {
            _abilityAttachRequesterTrigger.OnTriggered += OnTriggered;
        }

        private void UnregisterFromTrigger()
        {
            _abilityAttachRequesterTrigger.OnTriggered -= OnTriggered;
        }

        private void OnTriggered(TriggerBase trigger)
        {
            GameObject triggeredObject = ((Trigger_LayerBase) trigger).LastValidObject;

            AbilityAttachRequestorBase<T> abilityAttachRequestor =
                triggeredObject.GetComponent<AbilityAttachRequestorBase<T>>();

            if (abilityAttachRequestor == null)
            {
                return;
            }

            TryAttachAbility(abilityAttachRequestor.AbilityType);
        }
    }
}