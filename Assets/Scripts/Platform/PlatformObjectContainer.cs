using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using BLUE.ActionSystem;

namespace BLUE.ACubeStory.Platform
{
    public class PlatformObjectContainer : MonoBehaviour
    {
        private List<Transform> _objectsInPlatform = new List<Transform>();

        [SerializeField] private ActionExecutor _objectEnterActionExecutor = null;

        [SerializeField] private ActionExecutor _objectExitedActionExecutor = null;
        
        public Transform LastEnteredObject { get; private set; }
        public Transform LastExitedObject { get; private set; }
        
        private List<IPlatformEntrance> _entrancePoints;
        private List<IPlatformEntrance> _EntrancePoints
        {
            get
            {
                if (_entrancePoints == null)
                {
                    _entrancePoints = GetComponentsInChildren<IPlatformEntrance>().ToList();
                }

                return _entrancePoints;
            }
        }

        private void Awake()
        {
            RegisterToEntrancePoints();
        }

        private void OnDestroy()
        {
            UnregisterFromEntrancePoints();
        }

        private void RegisterToEntrancePoints()
        {
            foreach (IPlatformEntrance entrancePoint in _EntrancePoints)
            {
                entrancePoint.OnObjectEntered += OnObjectEnteredPlatform;
                entrancePoint.OnObjectExited += OnObjectExitedPlatform;
            }
        }

        private void UnregisterFromEntrancePoints()
        {
            foreach (IPlatformEntrance entrancePoint in _EntrancePoints)
            {
                entrancePoint.OnObjectEntered -= OnObjectEnteredPlatform;
                entrancePoint.OnObjectExited -= OnObjectExitedPlatform;
            }
        }

        private void OnObjectEnteredPlatform(Transform newObject)
        {
            if (_objectsInPlatform.Contains(newObject))
            {
                return;
            }
            
            _objectsInPlatform.Add(newObject);

            LastEnteredObject = newObject;
            
            _objectEnterActionExecutor.Execute();
        }

        private void OnObjectExitedPlatform(Transform newObject)
        {
            if (!_objectsInPlatform.Contains(newObject))
            {
                return;
            }

            _objectsInPlatform.Remove(newObject);

            LastExitedObject = newObject;
            
            _objectExitedActionExecutor.Execute();
        }
    }
}