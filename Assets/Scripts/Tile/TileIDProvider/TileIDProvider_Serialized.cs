using UnityEngine;

namespace BLUE.ACubeStory.TileSystem
{
    public class TileIDProvider_Serialized : MonoBehaviour,
        ITileIDProvider
    {
        [SerializeField] private int _tileID = 0;

        public int GetTileID()
        {
            return _tileID;
        }
    }
}