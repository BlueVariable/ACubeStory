using System;
using System.Collections.Generic;
using System.Linq;
using BLUE.ACubeStory.Ability;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class AbilityProvider_Cube : MonoBehaviour, IAbilityProvider
    {
        [SerializeField] private AbilityTypeInfo_Cube _cubeAbilityTypeInfo = null;
        
        private ECubeAbility[] _providedAbilityTypes = null;

        private AbilityBase[] _abilities;
        private AbilityBase[] _Abilities
        {
            get
            {
                if (_abilities == null)
                {
                    _abilities = Cube.Instance.GetComponentsInChildren<AbilityBase>();
                }

                return _abilities;
            }
        }

        public AbilityBase[] GetAbilities()
        {
            List<AbilityBase> abilities = new List<AbilityBase>();

            if (_providedAbilityTypes == null)
            {
                _providedAbilityTypes = _cubeAbilityTypeInfo.GetAbilityTypeInfoMapping().Keys.ToArray();
            }

            foreach (ECubeAbility cubeAbilityType in _providedAbilityTypes)
            {
                Type compType = _cubeAbilityTypeInfo.GetAbilityTypeInfoMapping()[cubeAbilityType];

                AbilityBase foundAbility = _Abilities.FirstOrDefault(val => val.GetType() == compType);

                abilities.Add(foundAbility);
            }

            return abilities.ToArray();
        }

        public void SetProvidedAbilityTypes(ECubeAbility[] targetAbilities)
        {
            _providedAbilityTypes = targetAbilities;
        }
    }
}