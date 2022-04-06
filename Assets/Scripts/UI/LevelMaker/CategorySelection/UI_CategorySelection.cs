using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class UI_CategorySelection : UIBase
    {
        [SerializeField] private UI_SelectedCategory _selectedCategory = null;

        [SerializeField] private UI_CategoryContainer _categoryContainer = null;

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