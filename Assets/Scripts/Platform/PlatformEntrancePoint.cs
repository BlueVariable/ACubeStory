using System;
using UnityEngine;

namespace BLUE.ACubeStory.Platform
{
    public abstract class PlatformEntrancePoint<TTargetComponent> : MonoBehaviour, IPlatformEntrance
    where TTargetComponent : MonoBehaviour 
    {
        public Action<Transform> OnObjectEntered { get; set; }
        public Action<Transform> OnObjectExited { get; set; }
        

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out TTargetComponent target))
            {
                return;
            }
            
            OnObjectEntered?.Invoke(other.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out TTargetComponent target))
            {
                return;
            }
            
            OnObjectExited?.Invoke(other.transform);
        }
    }
}