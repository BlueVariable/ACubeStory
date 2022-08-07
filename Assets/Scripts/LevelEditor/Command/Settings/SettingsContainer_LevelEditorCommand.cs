using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class SettingsContainer_LevelEditorCommand : MonoBehaviour
    {
        #region Singleton
        
        private static SettingsContainer_LevelEditorCommand _instance;
        public static SettingsContainer_LevelEditorCommand Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SettingsContainer_LevelEditorCommand>();
                }

                return _instance;
            }
        }
        
        #endregion
        
        [field: SerializeField] public SettingsSO_LevelEditorCommand CommandSO { get; private set; }
    }
}