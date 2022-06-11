using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class CubeEditorInputSettingsContainer : MonoBehaviour
    {
        private static CubeEditorInputSettingsContainer _instance;

        public static CubeEditorInputSettingsContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CubeEditorInputSettingsContainer>();
                }

                return _instance;
            }
        }
        
        [field: SerializeField] public CubeEditorInputSO InputSO { get; private set; }
    }
}