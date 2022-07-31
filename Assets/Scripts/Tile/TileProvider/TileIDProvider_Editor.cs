using UnityEngine;

namespace BLUE.ACubeStory.TileSystem
{
    public class TileIDProvider_Editor : MonoBehaviour, ITileIDProvider
    {
        #region Singleton
        
        private static TileIDProvider_Editor _instance;
        public static TileIDProvider_Editor Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<TileIDProvider_Editor>();
                }

                return _instance;
            }
        }

        #endregion
        
        public int SelectedTileID { get; set; }
        
        public int GetTileID()
        {
            return SelectedTileID;
        }
    }
}