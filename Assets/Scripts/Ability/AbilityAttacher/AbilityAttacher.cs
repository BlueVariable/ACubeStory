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
            
            Rewind_OnFall = 5,
            Rewind_OnObstacleHit = 6,
            Rewind_MergePuzzle = 7,
            
            Float = 8,
        }

        [Serializable]
        public class AbilityInfo
        {
            [SerializeField] private EAbility _abilityType = EAbility.None;
            public EAbility AbilityType => _abilityType;

            [SerializeField] private AbilityBase _ability = null;
            public AbilityBase Ability => _ability;
        }
        
        [SerializeField] private AttachableController_Ability _abilityAttachableController = null;
        
        [SerializeField] private Trigger_LayerBase _abilityAttachRequesterTrigger = null;
        
        [SerializeField] private AbilityInfo[] _abilityMap = null;

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

            AbilityAttachRequestorBase abilityAttachRequestor =
                triggeredObject.GetComponent<AbilityAttachRequestorBase>();

            if (abilityAttachRequestor == null)
            {
                return;
            }

            AbilityBase ability = _abilityMap.FirstOrDefault(i => i.AbilityType == abilityAttachRequestor.Ability).Ability;

            if (!_abilityAttachableController.TryAttach(ability))
            {
                return;
            }

            LastAttachedAbility = abilityAttachRequestor.Ability;
            
            _abilityAttachedActions.Execute();
        }
    }
}