using UnityEngine;

namespace BLUE.ACubeStory.LevelMaker
{
    public class Level : MonoBehaviour
    {
        public string LevelID { get; private set; }
        
        private LevelTileController _tileController;

        public LevelTileController TileController
        {
            get
            {
                if (_tileController == null)
                {
                    _tileController = new LevelTileController(transform);
                }

                return _tileController;
            }
        }

        public void InitLevel(string levelId)
        {
            LevelID = levelId;
        }
    }
}