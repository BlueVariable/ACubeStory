using System;
using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.Checkpoint
{
    public class CheckpointManager : MonoBehaviour
    {
        #region Singleton

        private static CheckpointManager _instance;
        public static CheckpointManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<CheckpointManager>();
                }

                return _instance;
            }
        }

        #endregion
        
        public Checkpoint LastCheckpoint { get; private set; }
        
        private readonly List<Checkpoint> _checkpoints = new();

        #region Events

        public Action<Checkpoint> OnCheckpointTriggered { get; set; }

        #endregion

        public void RegisterCheckpoint(Checkpoint checkpoint)
        {
            if (!_checkpoints.Contains(checkpoint))
            {
                _checkpoints.Add(checkpoint);

                checkpoint.OnTriggered += OnTriggered;
            }
        }
        
        public void UnregisterCheckpoint(Checkpoint checkpoint)
        {
            _checkpoints.Remove(checkpoint);
            
            checkpoint.OnTriggered -= OnTriggered;
        }

        private void OnTriggered(Checkpoint checkpoint)
        {
            LastCheckpoint = checkpoint;
            
            OnCheckpointTriggered?.Invoke(checkpoint);
        }
    }
}