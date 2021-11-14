using System;
using System.Collections.Generic;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class MergeableComposition : MonoBehaviour
    {
        [SerializeField] private TriggerBase[] _mergeRequestors = null;

        [SerializeField] private MergerBase _merger = null;

        [SerializeField] private Mergeable[] _initialMergeables = new Mergeable[0];

        private bool _isInitialized = false;

        private HashSet<Mergeable> _mergeables;
        private HashSet<Mergeable> _Mergeables
        {
            get
            {
                if (_mergeables == null)
                {
                    TryInitInitialMergeables();
                }

                return _mergeables;
            }
        }

        #region Events
        
        public Action<Mergeable> OnMerged { get; set; }

        #endregion

        private void Awake()
        {
            TryInitInitialMergeables();
            
            RegisterToMergeRequestors();

            RegisterToMerger();
        }

        private void OnDestroy()
        {
            UnregisterFromMergeRequestors();

            UnregisterFromMerger();
        }

        private bool TryInitInitialMergeables()
        {
            if (_isInitialized)
            {
                return false;
            }
            
            _mergeables = new HashSet<Mergeable>();
                    
            foreach (Mergeable initialMergeable in _initialMergeables)
            {
                _mergeables.Add(initialMergeable);

                OnMerged?.Invoke(initialMergeable);
            }

            return true;
        }

        private void RegisterToMergeRequestors()
        {
            foreach (TriggerBase mergeRequestor in _mergeRequestors)
            {
                mergeRequestor.OnTriggered += OnMergeRequested;
            }
        }

        private void UnregisterFromMergeRequestors()
        {
            foreach (TriggerBase mergeRequestor in _mergeRequestors)
            {
                mergeRequestor.OnTriggered -= OnMergeRequested;
            }
        }

        private void OnMergeRequested(TriggerBase trigger)
        {
            TryMerge();
        }

        private void TryMerge()
        {
            bool merged;
            
            do
            {
                merged = _merger.TryMerge(_Mergeables, out HashSet<Mergeable> newMergeables);
            }
            while(merged);
        }

        private void RegisterToMerger()
        {
            _merger.OnMerged += OnMergeableMerged;
        }

        private void UnregisterFromMerger()
        {
            _merger.OnMerged += OnMergeableMerged;
        }

        private void OnMergeableMerged(Mergeable oldMergeable, Mergeable newMergeable)
        {
            _Mergeables.Add(newMergeable);
            
            OnMerged?.Invoke(newMergeable);
        }
    }
}