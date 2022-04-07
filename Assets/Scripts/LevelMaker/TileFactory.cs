using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TileFactory : MonoBehaviour
    {
        private static TileFactory _instance;

        public static TileFactory Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<TileFactory>();
                }

                return _instance;
            }
        }

        public bool TryCreateTile(
            int tileId,
            Vector3 position,
            out Tile tile)
        {
            tile = null;

            if (!TileSettingsContainer.Instance.
                TileSettingsSo.TryGetTileSettings(tileId, out TileSettings tileSettings))
            {
                return false;
            }
            
            GameObject tileObject = Instantiate(tileSettings.Tile.gameObject);

            tileObject.transform.position = position;

            tile = tileObject.GetComponent<Tile>();
            
            // LevelMaker.Instance.CurLevel.TileController.SelectedContainer.AddTileIntoContainer(tile);
            
            return true;
        }
    }
}