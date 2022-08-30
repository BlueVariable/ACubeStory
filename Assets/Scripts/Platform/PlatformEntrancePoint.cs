using System;
using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.Platform
{
    public abstract class PlatformEntrancePoint : MonoBehaviour
    {
        public abstract Action<Transform> OnObjectEntered { get; set; }
        public abstract Action<Transform> OnObjectExited { get; set; }

        public readonly List<Transform> ObjectsOnPlatform = new List<Transform>();

        public abstract List<Transform> GetCurrentObjectsOnPlatform();

        public void UpdateObjectsOnPlatform(List<Transform> currentObjects)
        {
            ObjectsOnPlatform.Clear();
            
            ObjectsOnPlatform.AddRange(currentObjects);
        }

        public void UpdateExitedObjects(List<Transform> currentObjects)
        {
            foreach (Transform objectOnPlatform in ObjectsOnPlatform)
            {
                if (!currentObjects.Contains(objectOnPlatform))
                {
                    OnObjectExited?.Invoke(objectOnPlatform);
                }
            }
        }

        public void UpdateEnteredObjects(List<Transform> currentObjects)
        {
            foreach (Transform curObj in currentObjects)
            {
                if (ObjectsOnPlatform.Contains(curObj))
                {
                    continue;
                }

                OnObjectEntered?.Invoke(curObj);
            }
        }
    }
}