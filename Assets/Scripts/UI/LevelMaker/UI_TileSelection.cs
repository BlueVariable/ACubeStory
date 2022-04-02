using System;
using System.Collections.Generic;
using UnityEngine;
using BLUE.UISystem;
using BLUE.PoolingSystem;
using BLUE.ACubeStory.LevelMaker;

namespace BLUE.ACubeStory.UI
{
    public class UI_TileSelection : UIBase
    {
        [SerializeField] private Pool_MB _selectionPool = null;

        [SerializeField] private Transform _optionHolder = null;
        
        private List<UI_TileSelectionOption> _activatedOptions = new List<UI_TileSelectionOption>();

        public Action<int> OnSelectedTileUpdated { get; set; }

        protected override void ActivatingCustomActions(Action onCompleted = null)
        {
            InitTileOptions();
            
            base.ActivatingCustomActions(onCompleted);
        }

        protected override void DeactivatedCustomActions()
        {
            DisposeTileSelectionOptions();
            
            base.DeactivatedCustomActions();
        }

        protected override void DestroyCustomActions()
        {
            DisposeTileSelectionOptions();
            
            base.DestroyCustomActions();
        }
        
        private void InitTileOptions()
        {
            foreach (TileSettings tileSettings in TileSettingsContainer.Instance.TileSettingsSo.TileSettings)
            {
                UI_TileSelectionOption option = InitTileSelectionOption(tileSettings);
                
                _activatedOptions.Add(option);
                
                option.OnOptionSelected += OnTileOptionSelected;
                
                option.TryActivate();
            }
        }

        private UI_TileSelectionOption InitTileSelectionOption(TileSettings tileSettings)
        {
            PoolObject poolObject = _selectionPool.AllocatePoolObject();

            UI_TileSelectionOption option = poolObject.GetComponent<UI_TileSelectionOption>();

            option.transform.parent = _optionHolder;

            option.transform.localRotation = Quaternion.identity;
            
            option.InitOption(tileSettings);
            
            return option;
        }


        private void DisposeTileSelectionOptions()
        {
            foreach (UI_TileSelectionOption option in _activatedOptions)
            {
                option.OnOptionSelected += OnTileOptionSelected;

                option.TryDeactivate(() =>
                {
                    option.GetComponent<PoolObject>().Deallocate();
                });
            }
        }

        private void OnTileOptionSelected(int tileId)
        {
            OnSelectedTileUpdated?.Invoke(tileId);
        }
    }
}