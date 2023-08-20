using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.TileSystem
{
    public class TileFactory : MonoBehaviour
    {
        #region Singleton

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

        #endregion
        
        public bool TryCreateTile(
            int tileID,
            Vector3 position,
            out Tile tile)
        {
            tile = null;

            if (!SettingsContainer_Tile.Instance.
                TileSettingsSO.TryGetTileSettings(tileID, out TileSettings tileSettings))
            {
                return false;
            }

            GameObject tileObject = PrefabUtility.InstantiatePrefab(tileSettings.Tile.gameObject) as GameObject;

            tileObject.transform.position = position;

            tile = tileObject.GetComponent<Tile>();
            
            return true;
        }
    }
}