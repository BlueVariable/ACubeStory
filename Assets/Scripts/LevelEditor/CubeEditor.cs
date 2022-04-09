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
        
        [SerializeField] private ACubeStory.Level.Level _level = null;

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
                
        public ACubeStory.Level.Level Level
        {
            get => _level;

            set => _level = value;
        }

        public int SelectedTileID => _TileProvider.GetTileID();

        public TileGroup SelectedTileGroup => Level.transform.GetChild(0).transform.GetComponent<TileGroup>();
    }
}