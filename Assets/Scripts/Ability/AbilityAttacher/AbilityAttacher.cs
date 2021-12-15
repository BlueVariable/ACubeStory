using System;
using System.Linq;
using BLUE.ActionSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Ability
{
    public class AbilityAttacher : MonoBehaviour
    {
        public enum EAbility
        {
            None = 0,
            Jump = 1,
            Dash = 2,
            Roll_2D = 3,
            Roll_3D = 4,
        }

        [Serializable]
        public class AbilityMap
        {
            [SerializeField] private EAbility _abilityType = EAbility.None;
            public EAbility AbilityType => _abilityType;

            [SerializeField] private AbilityBase _ability = null;
            public AbilityBase Ability => _ability;
        }
        
        [SerializeField] private AttachableController_Ability _abilityAttachableController = null;
        
        [SerializeField] private Trigger_LayerBase _abilityAttachRequesterTrigger = null;
        
        [SerializeField] private AbilityMap[] _abilityMap = null;

        [SerializeField] private ActionExecutor _abilityAttachedActions = null;

        public EAbility LastAttachedAbility { get; private set; }
            
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

            AbilityAttachRequesterBase abilityAttachRequester =
                triggeredObject.GetComponent<AbilityAttachRequesterBase>();

            if (abilityAttachRequester == null)
            {
                return;
            }

            AbilityBase ability = _abilityMap.FirstOrDefault(i => i.AbilityType == abilityAttachRequester.Ability).Ability;

            if (!_abilityAttachableController.TryAttach(ability))
            {
                return;
            }

            LastAttachedAbility = abilityAttachRequester.Ability;
            
            _abilityAttachedActions.Execute();
        }
    }
}