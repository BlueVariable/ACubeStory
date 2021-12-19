using System;
using System.Collections.Generic;
using System.Linq;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class MergeableComposition : MonoBehaviour
    {
        [SerializeField] private MergerBase _merger = null;

        [SerializeField] private Mergeable[] _initialMergeables = new Mergeable[0];
        
        [SerializeField] private TriggerBase[] _collectiveMergeRequestors = null;

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
        public Action<Mergeable> OnUnmerged { get; set; }

        #endregion

        public bool TryUnmergeMergeablesExceptInitial()
        {
            bool unmerged = false;

            List<Mergeable> targetMergeables = new List<Mergeable>();
            
            foreach (Mergeable mergeable in Mergeables)
            {
                if (_initialMergeables.Contains(mergeable))
                {
                    continue;
                }

                targetMergeables.Add(mergeable);
            }
            
            foreach (Mergeable mergeable in targetMergeables)
            {
                if (TryUnmerge(mergeable))
                {
                    unmerged = true;
                }
            }

            return unmerged;
        }

        private void Awake()
        {
            TryInitInitialMergeables();
            
            RegisterToCollectiveMergeRequestors();
        }

        private void OnDestroy()
        {
            UnregisterFromCollectiveMergeRequestors();
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
                
                initialMergeable.Merged();

                OnMerged?.Invoke(initialMergeable);
            }

            return true;
        }

        private void RegisterToCollectiveMergeRequestors()
        {
            foreach (TriggerBase mergeRequestor in _collectiveMergeRequestors)
            {
                mergeRequestor.OnTriggered += OnCollectiveMergeRequested;
            }
        }

        private void UnregisterFromCollectiveMergeRequestors()
        {
            foreach (TriggerBase mergeRequestor in _collectiveMergeRequestors)
            {
                mergeRequestor.OnTriggered -= OnCollectiveMergeRequested;
            }
        }

        private void OnCollectiveMergeRequested(TriggerBase trigger)
        {
            PerformCollectiveMerge();
        }

        private void PerformCollectiveMerge()
        {
            bool merged;
            
            do
            {
                merged = _merger.TryCollectiveMerge(Mergeables, out HashSet<Mergeable> mergedMergeables);

                foreach (Mergeable mergeable in mergedMergeables)
                {
                    Mergeables.Add(mergeable);
                    
                    OnMerged?.Invoke(mergeable);
                }
            }
            while(merged);
        }
        
        private bool TryMerge(Mergeable targetMergeable)
        {
            bool merged = _merger.TryMerge(Mergeables, targetMergeable);

            if (merged)
            {
                Mergeables.Add(targetMergeable);

                OnMerged?.Invoke(targetMergeable);

                return true;
            }

            return false;
        }
        
        private bool TryUnmerge(Mergeable targetMergeable)
        {
            bool unmerged = _merger.TryUnmerge(Mergeables, targetMergeable);

            if (unmerged)
            {
                Mergeables.Remove(targetMergeable);

                OnUnmerged?.Invoke(targetMergeable);

                return true;
            }

            return false;
        }
    }
}