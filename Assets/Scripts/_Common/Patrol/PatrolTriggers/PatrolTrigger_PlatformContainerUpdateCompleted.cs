using System;
using BLUE.ACubeStory.Platform;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class PatrolTrigger_PlatformContainerUpdateCompleted : PatrolTriggerBase
    {
        [SerializeField] private PlatformObjectContainerUpdater _updater = null;
        
        public override Action OnPatrolTriggered { get; set; }

        private void Awake()
        {
            Application.targetFrameRate = 15;
            
            RegisterToUpdater();
        }

        private void OnDestroy()
        {
            UnregisterFromUpdater();
        }

        private void RegisterToUpdater()
        {
            _updater.OnUpdateCompleted += OnObjectContainerUpdated;
        }

        private void UnregisterFromUpdater()
        {
            _updater.OnUpdateCompleted -= OnObjectContainerUpdated;
        }

        private void OnObjectContainerUpdated()
        {
            Debug.Log($"Patrol Triggered -- Patrol Trigger Container Update Completed. {Time.frameCount}");
            
            OnPatrolTriggered?.Invoke();
        }
    }
}