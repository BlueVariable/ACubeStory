using UnityEngine;
using BLUE.ACubeStory.UI;

namespace BLUE.ACubeStory.LevelMaker
{
    public class TileProvider_UI : MonoBehaviour,
        ITileProvider
    {
        [SerializeField] private UI_TileSelection _tileSelectionUI = null;
        
        [SerializeField] private int _defaultSelectedTile = 0;

        private int _selectedTileID = 0;
        
        public Tile GetTile()
        {
            TileSettingsContainer.Instance.TileSettingsSo.TryGetTileSettings(_selectedTileID,
                out TileSettings settings);

            return settings.Tile;
        }

        private void Awake()
        {
            _selectedTileID = _defaultSelectedTile;
            
            RegisterToTileSelectionUI();
        }

        private void OnDestroy()
        {
            UnregisterFromTileSelectionUI();
        }

        private void RegisterToTileSelectionUI()
        {
            _tileSelectionUI.OnSelectedTileUpdated += OnSelectedTileUpdated;
        }

        private void UnregisterFromTileSelectionUI()
        {
            _tileSelectionUI.OnSelectedTileUpdated -= OnSelectedTileUpdated;
        }

        private void OnSelectedTileUpdated(int tileId)
        {
            _selectedTileID = tileId;
        }
    }
}