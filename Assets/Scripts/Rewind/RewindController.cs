using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BLUE.ACubeStory.Rewind
{
    public class RewindController : MonoBehaviour
    {
        [SerializeField] [Min(1)] private int _initialSaveBalanceFactor = 2;

        [SerializeField] private float _initialSaveBalanceMultiplier = 1;
        
        private List<IRewindable> _rewindables = new List<IRewindable>();

        private IEnumerator _saveSnapshotRoutine;

        private SnapshotSaveBalancer _saveBalancer;
        private SnapshotSaveBalancer _SaveBalancer 
        {
            get
            {
                if (_saveBalancer == null)
                {
                    _saveBalancer = new SnapshotSaveBalancer(
                        _initialSaveBalanceMultiplier,
                        _initialSaveBalanceFactor);
                }

                return _saveBalancer;
            }
        }
        
        #region Events

        public Action<IRewindable> OnRewindableRewound { get; set; }

        #endregion
        
        public void RegisterRewindable(IRewindable rewindable)
        {
            if (_rewindables.Contains(rewindable))
            {
                return;
            }
            
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
                    rewindable.TrySaveSnapshot(_SaveBalancer);
                }
                
                int frameCount = 0;

                while (frameCount < _SaveBalancer.SaveSnapshotFactor)
                {
                    frameCount++;
                    
                    yield return null;
                }
            }
        }
    }
}
