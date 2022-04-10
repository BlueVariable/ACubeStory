using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TileSettingsContainer : MonoBehaviour
    {
        private static TileSettingsContainer _instance;

        public static TileSettingsContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<TileSettingsContainer>();
                }

                return _instance;
            }
        }
        
        [SerializeField] private TileSettingsSO _tileSettingsSo = null;
        public TileSettingsSO TileSettingsSo => _tileSettingsSo;
    }
}