using UnityEngine;

namespace BLUE.ACubeStory.Ability
{
    public class AbilityProvider_Serialized : MonoBehaviour, IAbilityProvider
    {
        [SerializeField] private AbilityBase[] _abilities = null;
        
        public AbilityBase[] GetAbilities()
        {
            return _abilities;
        }
    }
}