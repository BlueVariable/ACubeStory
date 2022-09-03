using System;
using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.Platform
{
    public abstract class PlatformObjectContainerUpdater : MonoBehaviour
    {
        [SerializeField] private PlatformObjectContainer _objectContainer = null;

        public Action OnUpdateCompleted { get; set; }
        
        protected void UpdatePlatformObjectContainer()
        {
            Dictionary<PlatformEntrancePoint, List<Transform>> entranceObjectMappings =
                new Dictionary<PlatformEntrancePoint, List<Transform>>();

            List<PlatformEntrancePoint> entrancePoints = _objectContainer.EntrancePoints;

            foreach (PlatformEntrancePoint entrancePoint in entrancePoints)
            {
                entranceObjectMappings.Add(entrancePoint, entrancePoint.GetCurrentObjectsOnPlatform());
                
                entrancePoint.UpdateExitedObjects(entranceObjectMappings[entrancePoint]);
                
                Debug.Log($"Cur Objects {entrancePoint.transform.gameObject.name} == {entranceObjectMappings[entrancePoint].Count}");
            }

            foreach (PlatformEntrancePoint entrancePoint in entrancePoints)
            {
                entrancePoint.UpdateEnteredObjects(entranceObjectMappings[entrancePoint]);
                
                entrancePoint.UpdateObjectsOnPlatform(entranceObjectMappings[entrancePoint]);
            }
            
            OnUpdateCompleted?.Invoke();
        }
    }
}