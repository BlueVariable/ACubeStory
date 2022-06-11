using BLUE.ACubeStory.Merge;
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
        
        private ITileProvider _tileProvider;

        private ITileProvider _TileProvider
        {
            get
            {
                if (_tileProvider == null)
                {
                    _tileProvider = GetComponent<ITileProvider>();
                }

                return _tileProvider;
            }
        }
                
        public LevelHolder LevelHolder => FindObjectOfType<LevelHolder>();
        
        public int SelectedTileGroupIndex { get; set; }

        public int SelectedTileID => _TileProvider.GetTileID();

        public TileGroup SelectedTileGroup => LevelHolder.transform.GetChild(SelectedTileGroupIndex).transform.GetComponent<TileGroup>();
    }
}