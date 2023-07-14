using System;
using UnityEngine;

namespace BLUE.ACubeStory.Checkpoint
{
    public class Checkpoint : MonoBehaviour
    {
        #region Events

        public Action<Checkpoint> OnTriggered { get; set; }

        #endregion
        
        public void TriggerCheckpoint()
        {
            OnTriggered?.Invoke(this);
        }

        private void Awake()
        {
            CheckpointManager.Instance.RegisterCheckpoint(this);
        }

        private void OnDestroy()
        {
            if (CheckpointManager.Instance == null)
            {
                return;
            }
            
            CheckpointManager.Instance.UnregisterCheckpoint(this);
        }
    }
}