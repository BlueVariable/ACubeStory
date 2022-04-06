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

        private List<UI_CategoryContainerItem> _activatedItems = new List<UI_CategoryContainerItem>();

        protected override void ActivatingCustomActions(Action onCompleted = null)
        {
            InitCategoryContainerItems();
            
            RegisterToLevelMakerEvents();
            
            base.ActivatingCustomActions(onCompleted);
        }

        protected override void DeactivatingCustomActions(Action onCompleted = null)
        {
            RegisterToLevelMakerEvents();
            
            base.DeactivatingCustomActions(onCompleted);
        }

        protected override void DeactivatedCustomActions()
        {
            DisposeActivatedItems();
            
            base.DeactivatedCustomActions();
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

        private void InitLevelContainers()
        {
            
        }
    }
}