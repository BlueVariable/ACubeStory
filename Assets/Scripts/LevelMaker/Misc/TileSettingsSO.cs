using System;
using System.Linq;
using UnityEngine;

namespace BLUE.ACubeStory.LevelMaker
{
    [Serializable]
    public class TileSettings
    {
        [SerializeField] private int _tileID = -1;
        public int TileID => _tileID;

        [SerializeField] private Tile _tile = null;
        public Tile Tile => _tile;

        [SerializeField] private string _displayName = string.Empty;
        public string DisplayName => _displayName;

        public void InitDisplayName()
        {
            if (Tile != null && _displayName.Equals(string.Empty))
            {
                _displayName = Tile.gameObject.name;
            }
        }
    }
    
    [CreateAssetMenu(fileName = "TileSettingsSO", menuName = "BLUE/Settings/Level Maker/Create New Tile Settings")]
    public class TileSettingsSO : ScriptableObject
    {
        [SerializeField] private TileSettings[] _tiles = null;
        
        public TileSettings[] TileSettings => _tiles;

        public bool TryGetTileSettings(int tileId, out TileSettings tileSettings)
        {
            tileSettings = _tiles.SingleOrDefault(i => i.TileID == tileId);

            return tileSettings != null;
        }
        
        private void OnValidate()
        {
            if (_tiles == null)
            {
                return;
            }

            foreach (TileSettings tileSettings in _tiles)
            {
                tileSettings.InitDisplayName();
            }
        }
    }
}