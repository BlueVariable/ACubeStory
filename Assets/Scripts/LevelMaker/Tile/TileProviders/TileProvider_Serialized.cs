using UnityEngine;

namespace BLUE.ACubeStory.LevelMaker
{
    public class TileProvider_Serialized : MonoBehaviour,
        ITileProvider
    {
        [SerializeField] private Tile _tile = null;

        public Tile GetTile()
        {
            return _tile;
        }
    }
}