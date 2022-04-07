using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TileProvider_Serialized : MonoBehaviour,
        ITileProvider
    {
        [SerializeField] private int _tileId = 0;

        public int GetTileID()
        {
            return _tileId;
        }
    }
}