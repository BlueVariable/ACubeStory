using System;
using BLUE.UISystem;
using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class UI_SelectedCategory : UIBase
    {
        [SerializeField] private UI_Text _selectedContainerNameText = null;

        [SerializeField] private ButtonBase _categoryToggleButton = null;

        [SerializeField] private UI_CategoryContainer _categoryContainer = null;
        
        protected override void AwakeCustomActions()
        {
            RegisterToLevelMakerEvents();
            
            InitContainerName();
            
            base.AwakeCustomActions();
        }

        protected override void DestroyCustomActions()
        {
            UnregisterFromLevelMakerEvents();
            
            base.DestroyCustomActions();
        }

        protected override void ActivatingCustomActions(Action onCompleted = null)
        {
            ActivateSubUIElements();
            
            RegisterToCategoryToggleButton();
            
            base.ActivatingCustomActions(onCompleted);
        }

        protected override void DeactivatingCustomActions(Action onCompleted = null)
        {
            DeactivateSubUIElements();
            
            UnregisterFromCategoryToggleButton();
            
            base.DeactivatingCustomActions(onCompleted);
        }

        private void ActivateSubUIElements()
        {
            _selectedContainerNameText.TryActivate();

            _categoryToggleButton.TryActivate();
        }

        private void DeactivateSubUIElements()
        {
            _selectedContainerNameText.TryDeactivate();

            _categoryToggleButton.TryDeactivate();
        }

        private void RegisterToLevelMakerEvents()
        {
            LevelMaker.LevelMaker.Instance.CurLevel.TileController.OnCurTileContainerUpdated +=
                OnSelectedTileContainerUpdated;
        }

        private void UnregisterFromLevelMakerEvents()
        {
            LevelMaker.LevelMaker.Instance.CurLevel.TileController.OnCurTileContainerUpdated -=
                OnSelectedTileContainerUpdated;
        }

        private void RegisterToCategoryToggleButton()
        {
            _categoryToggleButton.OnClick += OnClickedCategoryToggleButton;
        }

        private void UnregisterFromCategoryToggleButton()
        {
            _categoryToggleButton.OnClick -= OnClickedCategoryToggleButton;
        }

        private void OnClickedCategoryToggleButton(ButtonBase button)
        {
            if (_categoryContainer.Status == EStatus.Activated)
            {
                _categoryContainer.TryDeactivate();
            }    
            else if (_categoryContainer.Status == EStatus.Deactivated)
            {
                _categoryContainer.TryActivate();
            }
        }

        private void OnSelectedTileContainerUpdated()
        {
            InitContainerName();
        }

        private void InitContainerName()
        {
            _selectedContainerNameText.Text =
                LevelMaker.LevelMaker.Instance.CurLevel.TileController.SelectedContainer.ContainerName;
        }
    }
}
