using BLUE.ACubeStory.Merge;
using BLUE.ACubeStory.TileSystem;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class CubeEditor : MonoBehaviour
    {
        private static CubeEditor _instance;

        public static CubeEditor Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CubeEditor>();
                }

                return _instance;
            }
        }

        public CubeEditorInputSO InputSo => CubeEditorInputSettingsContainer.Instance.InputSO;

        [SerializeField] private MergePuzzle _defaultMergePuzzle = null;
        public MergePuzzle DefaultMergePuzzle => _defaultMergePuzzle;

        [SerializeField] private bool _displayToolTips = false;
        public bool DisplayToolTips => _displayToolTips;
        
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
                
        public LevelEditorContainer LevelEditorContainer => FindObjectOfType<LevelEditorContainer>();
        
        public int SelectedTileGroupIndex { get; set; }

        public int SelectedTileID => TileIDProvider.GetTileID();

        public TileGroupEditorContainer SelectedTileGroupEditorContainer => LevelEditorContainer.transform.GetChild(SelectedTileGroupIndex).transform.GetComponent<TileGroupEditorContainer>();
    }
}