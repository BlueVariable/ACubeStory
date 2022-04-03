using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.LevelMaker
{
    public class TileContainer : MonoBehaviour
    {
        public string ContainerName { get; private set; }

        private List<Tile> _tiles = new List<Tile>();

        public void InitContainer(string name)
        {
            ContainerName = name;

            gameObject.name = name;
        }

        public void AddTileIntoContainer(Tile tile)
        {
            tile.transform.parent = transform;
            
            _tiles.Add(tile);
        }
    }
}