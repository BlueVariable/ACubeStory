using System;
using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.LevelMaker
{
    public class LevelTileController
    {
        private const string DEFAULT_TILE_CONTAINER_NAME = "Base";
        
        private Transform _tileContainerHolder;

        public TileContainer SelectedContainer { get; private set; }

        private List<TileContainer> _tileContainers = new List<TileContainer>();

        public List<TileContainer> TileContainers => _tileContainers;

        public Action OnCurTileContainerUpdated { get; set; }
        
        public Action OnNewContainerAdded { get; set; }

        public LevelTileController(Transform tileContainerHolder)
        {
            _tileContainerHolder = tileContainerHolder;

            TryAddNewTileContainer(DEFAULT_TILE_CONTAINER_NAME, out TileContainer tileContainer);

            SelectedContainer = tileContainer;
            
            OnCurTileContainerUpdated?.Invoke();
        }

        public bool TryAddNewTileContainer(string containerName, out TileContainer tileContainer)
        {
            GameObject containerObject = new GameObject();

            containerObject.transform.parent = _tileContainerHolder;

            containerObject.name = containerName;

            tileContainer = containerObject.AddComponent<TileContainer>();
            
            tileContainer.InitContainer(containerName);
            
            _tileContainers.Add(tileContainer);
            
            OnNewContainerAdded?.Invoke();

            return true;
        }

        public void SelectTileContainer(string containerName)
        {
            foreach (TileContainer container in _tileContainers)
            {
                if (container.ContainerName.Equals(containerName))
                {
                    SelectedContainer = container;
                    
                    OnCurTileContainerUpdated?.Invoke();
                    
                    return;
                }
            }
        }
    }
}