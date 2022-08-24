using System;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Platform
{
    public class PlatformEntrancePoint : MonoBehaviour
    {
        [SerializeField] private Trigger_OnTriggerEnter _enterTrigger = null;

        [SerializeField] private Trigger_OnTriggerExit _exitTrigger = null;
        
        public Action<Transform> OnObjectEntered { get; set; }
        
        public Action<Transform> OnObjectExited { get; set; }

        private void Awake()
        {
            RegisterToTriggers();
        }

        private void OnDestroy()
        {
            UnregisterFromTriggers();
        }

        private void RegisterToTriggers()
        {
            _enterTrigger.OnTriggered += OnEnterTriggered;
            _exitTrigger.OnTriggered += OnExitTriggered;
        }

        private void UnregisterFromTriggers()
        {
            _enterTrigger.OnTriggered -= OnEnterTriggered;
            _exitTrigger.OnTriggered -= OnExitTriggered;
        }

        private void OnEnterTriggered(TriggerBase trigger)
        {
            OnObjectEntered?.Invoke(_enterTrigger.LastValidCollider.transform);
        }

        private void OnExitTriggered(TriggerBase trigger)
        {
            OnObjectExited?.Invoke(_exitTrigger.LastValidCollider.transform);
        }
    }
}