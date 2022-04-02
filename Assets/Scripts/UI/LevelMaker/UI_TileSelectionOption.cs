using System;
using BLUE.UISystem;
using BLUE.ACubeStory.LevelMaker;
using BLUE.Extensions;
using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class UI_TileSelectionOption : UIBase
    {
        [SerializeField] private Transform _tileHolder = null;

        [SerializeField] private ButtonBase _button = null;

        [SerializeField] private int _uiLayer = default;
        
        private int _curTileId;
        
        public Action<int> OnOptionSelected { get; set; }
        
        public void InitOption(TileSettings tileSettings)
        {
            DestroyCurTile();

            _curTileId = tileSettings.TileID;
            
            InitCurTile(tileSettings);
        }

        protected override void ActivatedCustomActions()
        {
            RegisterToSelectionButton();

            _button.TryActivate();
            
            base.ActivatedCustomActions();
        }

        protected override void DeactivatingCustomActions(Action onCompleted = null)
        {
            UnregisterFromSelectionButton();
            
            _button.TryDeactivate();
            
            base.DeactivatingCustomActions(onCompleted);
        }

        private void RegisterToSelectionButton()
        {
            _button.OnClick += OnPressedOptionButton;
        }

        private void UnregisterFromSelectionButton()
        {
            _button.OnClick -= OnPressedOptionButton;
        }

        private void OnPressedOptionButton(ButtonBase button)
        {
            OnOptionSelected?.Invoke(_curTileId);
        }

        private void DestroyCurTile()
        {
            Tile curTile = _tileHolder.GetComponentInChildren<Tile>();
            
            Destroy(curTile.gameObject);
        }

        private void InitCurTile(TileSettings settings)
        {
            GameObject gameObject = Instantiate(settings.Tile.gameObject, _tileHolder);

            gameObject.SetLayer(_uiLayer);
        }
    }
}