using BLUE.UISystem;
using UnityEngine.UI;

namespace BLUE.ACubeStory.UI
{
    public class UI_InputField : UIBase
    {
        private InputField _inputField;
        
        private InputField _InputField 
        {
            get
            {
                if (_inputField == null)
                {
                    _inputField = GetComponentInChildren<InputField>();
                }

                return _inputField;
            }
        }

        protected override void AwakeCustomActions()
        {
            base.AwakeCustomActions();
        }

        protected override void DestroyCustomActions()
        {
            base.DestroyCustomActions();
        }
    }
}