using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    [Serializable]
    public class TileSettings
    {
        [SerializeField] private int _tileID = -1;
        public int TileID
        {
            get => _tileID;
            set => _tileID = value;
        }

        [SerializeField] private Tile _tile = null;
        public Tile Tile => _tile;

        [SerializeField] private string _displayName = string.Empty;
        public string DisplayName => _displayName;

        public void InitDisplayName()
        {
            if (Tile != null)
            {
                _displayName = Tile.gameObject.name;
            }
        }
    }
    
    [CreateAssetMenu(fileName = "TileSettingsSO", menuName = "BLUE/Settings/Level Maker/Create New Tile Settings")]
    public class TileSettingsSO : ScriptableObject
    {
        [SerializeField] private List<TileSettings> _tiles = null;
        
        public List<TileSettings> TileSettings => _tiles;

        [ContextMenu("Validate Scriptable Object")]
        public void ValidateSO()
        {
            ValidateScriptableObject();
        }

        public bool TryGetTileSettings(int tileId, out TileSettings tileSettings)
        {
            tileSettings = _tiles.SingleOrDefault(i => i.TileID == tileId);

            return tileSettings != null;
        }
        
        private void OnValidate()
        {
            ValidateScriptableObject();
        }

        private void ValidateScriptableObject()
        {
            if (_tiles == null)
            {
                return;
            }

            foreach (TileSettings tileSettings in _tiles)
            {
                tileSettings.InitDisplayName();

                tileSettings.TileID = _tiles.IndexOf(tileSettings);
            }
        }
    }
}