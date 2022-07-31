using UnityEngine;

namespace BLUE.ACubeStory.TileSystem
{
    public enum ETileCategory
    {
        None = 0,
        Ground = 10,
        Mergeable = 20,
        MergeableSlot = 30,
        Checkpoint = 40,
    }

    public class Tile : MonoBehaviour
    {
        [SerializeField] private ETileCategory _tileCategory = ETileCategory.None;
        public ETileCategory TileCategory => _tileCategory;
    }
}