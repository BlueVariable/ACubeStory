using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BLUE.ACubeStory.TileSystem
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
    
    [CreateAssetMenu(fileName = "SettingsSO_Tile", menuName = "BLUE/Settings/Level Editor/Create SettingsSO_Tile")]
    public class SettingsSO_Tile : ScriptableObject
    {
        [SerializeField] private List<TileSettings> _tileSettingsColl = null;
        public List<TileSettings> TileSettingsColl => _tileSettingsColl;

        [ContextMenu("Validate Scriptable Object")]
        public void ValidateSO()
        {
            if (_tileSettingsColl == null)
            {
                return;
            }

            foreach (TileSettings tileSettings in _tileSettingsColl)
            {
                tileSettings.InitDisplayName();

                tileSettings.TileID = _tileSettingsColl.IndexOf(tileSettings);
            }
        }

        public bool TryGetTileSettings(int tileID, out TileSettings tileSettings)
        {
            tileSettings = _tileSettingsColl.SingleOrDefault(i => i.TileID == tileID);

            return tileSettings != null;
        }
        
        private void OnValidate()
        {
            ValidateSO();
        }
    }
}