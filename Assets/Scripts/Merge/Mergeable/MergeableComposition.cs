using System;
using System.Collections.Generic;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class MergeableComposition : MonoBehaviour
    {
        [SerializeField] private MergerBase _merger = null;

        [SerializeField] private Mergeable[] _initialMergeables = new Mergeable[0];
        
        [SerializeField] private TriggerBase[] _mergeRequestors = null;

        private HashSet<Mergeable> _mergeables;
        public HashSet<Mergeable> Mergeables
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
        
        private bool _isInitialized = false;

        #region Events
        
        public Action<Mergeable> OnMerged { get; set; }

        #endregion

        private void Awake()
        {
            TryInitInitialMergeables();
            
            RegisterToMergeRequestors();
        }

        private void OnDestroy()
        {
            UnregisterFromMergeRequestors();
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
                merged = _merger.TryMerge(Mergeables, out HashSet<Mergeable> mergedMergeables);

                foreach (Mergeable mergeable in mergedMergeables)
                {
                    _mergeables.Add(mergeable);
                    
                    OnMerged?.Invoke(mergeable);
                }
            }
            while(merged);
        }
    }
}