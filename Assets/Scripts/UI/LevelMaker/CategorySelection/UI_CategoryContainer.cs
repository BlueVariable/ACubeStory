using System;
using System.Collections.Generic;
using BLUE.ACubeStory.LevelMaker;
using BLUE.PoolingSystem;
using BLUE.UISystem;
using UnityEngine;
                     
namespace BLUE.ACubeStory.UI
{
    public class UI_CategoryContainer : UIBase
    {
        [SerializeField] private Pool_MB _categoryObjectPool = null;

        [SerializeField] private ButtonBase _addCategoryButton = null;

        [SerializeField] private Transform _itemContainer = null;

        [SerializeField] private UI_CategorySelection _categorySelection = null;
        
        private List<UI_CategoryContainerItem> _activatedItems = new List<UI_CategoryContainerItem>();

        protected override void ActivatingCustomActions(Action onCompleted = null)
        {
            InitCategoryContainerItems();
            
            RegisterToLevelMakerEvents();

            RegisterToAddNewCategoryButton();

            _addCategoryButton.TryActivate();
            
            base.ActivatingCustomActions(onCompleted);
        }

        protected override void DeactivatingCustomActions(Action onCompleted = null)
        {
            UnregisterFromLevelMakerEvents();
            
            UnregisterFromAddNewCategoryButton();
            
            _addCategoryButton.TryDeactivate();
            
            base.DeactivatingCustomActions(onCompleted);
        }

        protected override void DeactivatedCustomActions()
        {
            DisposeActivatedItems();
            
            base.DeactivatedCustomActions();
        }

        private void RegisterToAddNewCategoryButton()
        {
            _addCategoryButton.OnClick += OnPressedNewCategoryButton;
        }

        private void UnregisterFromAddNewCategoryButton()
        {
            _addCategoryButton.OnClick -= OnPressedNewCategoryButton;
        }

        private void OnPressedNewCategoryButton(ButtonBase button)
        {
            _categorySelection.ActivateCategoryAddInputField();
            
            TryDeactivate();
        }

        private void RegisterToLevelMakerEvents()
        {
            LevelMaker.LevelMaker.Instance.CurLevel.TileController.OnNewContainerAdded +=
                OnNewContainerAdded;
        }

        private void UnregisterFromLevelMakerEvents()
        {
            LevelMaker.LevelMaker.Instance.CurLevel.TileController.OnCurTileContainerUpdated -=
                OnNewContainerAdded;
        }

        private void OnNewContainerAdded()
        {
            DisposeActivatedItems();
            
            InitCategoryContainerItems();
        }

        private void InitCategoryContainerItems()
        {
            foreach (TileContainer tileContainer in LevelMaker.LevelMaker.Instance.CurLevel.TileController.TileContainers)
            {
                PoolObject_MB poolObject = (PoolObject_MB) _categoryObjectPool.AllocatePoolObject();

                UI_CategoryContainerItem item = poolObject.GetComponent<UI_CategoryContainerItem>();

                item.InitItem(tileContainer, _itemContainer);
                
                item.TryActivate();
                
                _activatedItems.Add(item);
            }
            
            _addCategoryButton.transform.SetAsLastSibling();
        }

        private void DisposeActivatedItems()
        {
            foreach (UI_CategoryContainerItem containerItem in _activatedItems)
            {
                containerItem.GetComponent<PoolObject_MB>().Deallocate();

                containerItem.TryDeactivate();
            }
            
            _activatedItems.Clear();
        }
    }
}