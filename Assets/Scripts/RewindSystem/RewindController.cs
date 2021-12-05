using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.RewindSystem
{
    public class RewindController : MonoBehaviour
    {
        private List<IRewindable> _rewindables = new List<IRewindable>();

        private IEnumerator _saveSnapshotRoutine;

        private IEnumerator _rewindRoutine;
        
        public void RegisterRewindable(IRewindable rewindable)
        {
            _rewindables.Add(rewindable);
        }

        public void UnregisterRewindable(IRewindable rewindable)
        {
            if (_rewindables.Contains(rewindable))
            {
                _rewindables.Remove(rewindable);
            }
        }

        public void ResetSnapShots()
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
                rewindable.StartRewind();
            }
        }

        public void StopRewind()
        {
            foreach (IRewindable rewindable in _rewindables)
            {
                rewindable.StopRewind();
            }
        }

        private IEnumerator SaveSnapshotProgress()
        {
            while (true)
            {
                foreach (IRewindable rewindable in _rewindables)
                {
                    rewindable.SaveSnapshot();
                }
                
                yield return null;
            }
        }
    }
}