using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.Rewind
{
    public class RewindController : MonoBehaviour
    {
        [SerializeField] [Min(1)] private int _frameSaveInterval = 5;
        
        private List<IRewindable> _rewindables = new List<IRewindable>();

        private IEnumerator _saveSnapshotRoutine;
        
        #region Events

        public Action<IRewindable> OnRewindableRewound { get; set; }

        #endregion
        
        public void RegisterRewindable(IRewindable rewindable)
        {
            _rewindables.Add(rewindable);
        }

        public void UnregisterRewindable(IRewindable rewindable)
        {
            _rewindables.Remove(rewindable);
        }

        public void ResetSnapshots()
        {
            foreach (IRewindable rewindable in _rewindables)
            {
                rewindable.ResetSavedSnapshots();
            }
        }

        public void StartSaveSnapshotRoutine()
        {
            StopSaveSnapshotRoutine();

            _saveSnapshotRoutine = SaveSnapshotProgress();

            StartCoroutine(_saveSnapshotRoutine);
        }

        public void StopSaveSnapshotRoutine()
        {
            if (_saveSnapshotRoutine != null)
            {
                StopCoroutine(_saveSnapshotRoutine);
            }
        }

        public void StartRewind()
        {
            foreach (IRewindable rewindable in _rewindables)
            {
                rewindable.OnRewound += OnRewound;
                
                rewindable.StartRewind();
            }
        }

        public void StopRewind()
        {
            foreach (IRewindable rewindable in _rewindables)
            {
                rewindable.OnRewound -= OnRewound;
                
                rewindable.StopRewind();
            }
        }

        private void OnRewound(IRewindable rewindable)
        {
            OnRewindableRewound?.Invoke(rewindable);
        }

        private IEnumerator SaveSnapshotProgress()
        {
            while (true)
            {
                foreach (IRewindable rewindable in _rewindables)
                {
                    rewindable.SaveSnapshot();
                }
                
                int frameCount = 0;

                while (frameCount < _frameSaveInterval)
                {
                    frameCount++;
                    
                    yield return null;
                }
            }
        }
    }
}