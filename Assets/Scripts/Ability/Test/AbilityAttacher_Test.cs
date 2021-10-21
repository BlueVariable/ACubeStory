using UnityEngine;

namespace BLUE.ACubeStory.Ability
{
    public class AbilityAttacher_Test : MonoBehaviour
    {
        [SerializeField] private AttachableController_Ability _abilityAttachableController = null;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _abilityAttachableController.TryAttach<Ability_Test>();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _abilityAttachableController.TryDetach<Ability_Test>();
            }
        }
    }
}