using UnityEngine;

namespace BLUE.ACubeStory.TileSystem
{
    public class SettingsContainer_Tile : MonoBehaviour
    {
        #region Singleton
        
        private static SettingsContainer_Tile _instance;
        public static SettingsContainer_Tile Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SettingsContainer_Tile>();
                }

                return _instance;
            }
        }
        
        #endregion
        
        [SerializeField] private SettingsSO_Tile _tileSettingsSO = null;
        public SettingsSO_Tile TileSettingsSO => _tileSettingsSO;
    }
}