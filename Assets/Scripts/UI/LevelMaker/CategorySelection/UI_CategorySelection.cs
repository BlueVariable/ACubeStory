using BLUE.UISystem;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BLUE.ACubeStory.UI
{
    public class UI_CategorySelection : UIBase
    {
        [SerializeField] private UI_SelectedCategory _selectedCategory = null;

        // [SerializeField] private UI_CategoryContainer _categoryContainer = null;

        [SerializeField] private UI_InputField _inputField = null;
        
        public void ActivateCategoryAddInputField()
        {
            _inputField.TryActivate();

            EventSystem.current.SetSelectedGameObject(_inputField.gameObject);
        }
        
        protected override void AwakeCustomActions()
        {
            _selectedCategory.TryActivate();
            
            base.AwakeCustomActions();
        }

        protected override void DestroyCustomActions()
        {
            _selectedCategory.TryDeactivate();
            
            base.DestroyCustomActions();
        }
    }
}