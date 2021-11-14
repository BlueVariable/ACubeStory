using System;
using System.Collections.Generic;
using System.Linq;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    // TODO: Unmerge
    public abstract class MergerBase : MonoBehaviour
    {
        [SerializeField] private Transform _mergerRoot = null;
        
        [SerializeField] private ActionExecutor _mergedActionExecutor = null;

        [SerializeField] private MergeConditionBase[] _mergeConditions = new MergeConditionBase[0];

        #region Events

        public Action<Mergeable, Mergeable> OnMerged { get; set; }

        #endregion
        
        protected abstract HashSet<Mergeable> GetCandidateMergeables(Mergeable formerMergeable);

        protected virtual void MergeCustomActions(Mergeable formerMergeable, Mergeable mergedMergeable) { }

        public bool TryMerge(HashSet<Mergeable> formerMergeables, out HashSet<Mergeable> mergedMergeables)
        {
            mergedMergeables = new HashSet<Mergeable>();

            bool merged = false;
            
            foreach (Mergeable formerMergeable in formerMergeables)
            {
                HashSet<Mergeable> candidateMergeables = GetCandidateMergeables(formerMergeable);

                candidateMergeables.RemoveWhere(formerMergeables.Contains);

                foreach (Mergeable candidateMergeable in candidateMergeables)
                {
                    if (_mergeConditions.All(val 
                        => val.CanMerge(formerMergeable, candidateMergeable)))
                    {
                        Merge(formerMergeable, candidateMergeable);

                        mergedMergeables.Add(candidateMergeable);

                        merged = true;
                    }
                }
            }

            return merged;
        }

        private void Merge(Mergeable formerMergeable, Mergeable mergedMergeable)
        {
            mergedMergeable.MergeableObject.parent = _mergerRoot;
            
            MergeCustomActions(formerMergeable, mergedMergeable);
            
            OnMerged?.Invoke(formerMergeable, mergedMergeable);
            
            _mergedActionExecutor.Execute();
        }
    }
}