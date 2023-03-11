using BLUE.ACubeStory.Ability;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class AbilityAttachableControllerProvider_Cube : MonoBehaviour, IAbilityAttachableControllerProvider
    {
        private AttachableController_Ability _abilityAttachableController;
        private AttachableController_Ability _AbilityAttachableController
        {
            get
            {
                if (_abilityAttachableController == null)
                {
                    _abilityAttachableController = Cube.Instance.GetComponentInChildren<AttachableController_Ability>();
                }

                return _abilityAttachableController;
            }
        }
        
        public AttachableController_Ability GetAbilityAttachableController()
        {
            return _AbilityAttachableController;
        }
    }
}