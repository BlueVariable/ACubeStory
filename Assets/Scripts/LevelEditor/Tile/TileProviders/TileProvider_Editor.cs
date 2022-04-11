using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TileProvider_Editor : MonoBehaviour, ITileProvider
    {
        private static TileProvider_Editor _instance;

        public static TileProvider_Editor Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<TileProvider_Editor>();
                }

                return _instance;
            }
        }
        
        public int SelectedTileID { get; set; }
        
        public int GetTileID()
        {
            return SelectedTileID;
        }
    }
}