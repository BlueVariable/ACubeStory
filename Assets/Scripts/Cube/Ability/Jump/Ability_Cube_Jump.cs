using System.Linq;
using BLUE.ACubeStory.Ability;
using BLUE.InputSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Ability_Cube_Jump : AbilityBase
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;

        [SerializeField] private KeyCode[] _jumpKeyCodes = default;
        
        public override void Attached()
        {
            RegisterToInputManager();
        }

        public override void Detached()
        {
            UnregisterFromInputManager();
        }
        
        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>
                (EInputEvent.KeyDown, OnKeyDownInputPerformed);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>
                (EInputEvent.KeyDown, OnKeyDownInputPerformed);
        }

        private void OnKeyDownInputPerformed(InputData_Key keyInputData)
        {
            if (_jumpKeyCodes.Contains(keyInputData.KeyCode))
            {
                _cubeFSM.TryChangeState(ECubeState.Jump);
            }
        }
    }
}
