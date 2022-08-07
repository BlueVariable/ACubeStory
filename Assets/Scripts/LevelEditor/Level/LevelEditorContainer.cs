using BLUE.ACubeStory.Merge;
using BLUE.ACubeStory.TileSystem;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class LevelEditorContainer : MonoBehaviour
    {
        #region Singleton

        private static LevelEditorContainer _instance;
        public static LevelEditorContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<LevelEditorContainer>();
                }

                return _instance;
            }
        }

        #endregion
        
        [SerializeField] private bool _displayTooltips = false;
        public bool DisplayTooltips => _displayTooltips;
        
        [SerializeField] private MergePuzzle _mergePuzzleRef = null;
        public MergePuzzle MergePuzzleRef => _mergePuzzleRef;

        public int SelectedTileGroupIndex { get; set; }
        
        public SettingsSO_LevelEditorCommand LevelEditorCommandSO => SettingsContainer_LevelEditorCommand.Instance.CommandSO;
        
        public EditorLevelRef EditorLevelRef => FindObjectOfType<EditorLevelRef>();
        
        public int SelectedTileID => TileIDProvider.GetTileID();
        
        public TileGroupEditorContainer SelectedTileGroupEditorContainer => EditorLevelRef.transform
            .GetChild(SelectedTileGroupIndex).transform.GetComponent<TileGroupEditorContainer>();
        
        private ITileIDProvider _tileIDProvider;
        private ITileIDProvider TileIDProvider
        {
            get
            {
                if (_tileIDProvider == null)
                {
                    _tileIDProvider = GetComponent<ITileIDProvider>();
                }

                return _tileIDProvider;
            }
        }
    }
}