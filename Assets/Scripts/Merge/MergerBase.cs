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
        [SerializeField] private ActionExecutor _mergedActionExecutor = null;

        [SerializeField] private MergeConditionBase[] _mergeConditions = new MergeConditionBase[0];

        #region Events

        public Action<Mergeable, Mergeable> OnMerged { get; set; }

        #endregion
        
        protected abstract HashSet<Mergeable> GetCandidateMergeables(Mergeable oldMergeable);

        protected virtual void MergeCustomActions(Mergeable oldMergeable, Mergeable newMergeable) { }

        public bool TryMerge(HashSet<Mergeable> oldMergeables, out HashSet<Mergeable> newMergeables)
        {
            newMergeables = new HashSet<Mergeable>();

            bool merged = false;
            
            foreach (Mergeable mergedMergeable in oldMergeables)
            {
                HashSet<Mergeable> candidateMergeables = GetCandidateMergeables(mergedMergeable);

                candidateMergeables.RemoveWhere(oldMergeables.Contains);

                foreach (Mergeable candidateMergeable in candidateMergeables)
                {
                    if (_mergeConditions.All(val 
                        => val.CanMerge(mergedMergeable, candidateMergeable)))
                    {
                        Merge(mergedMergeable, candidateMergeable);

                        newMergeables.Add(candidateMergeable);

                        merged = true;
                    }
                }
            }

            return merged;
        }

        private void Merge(Mergeable oldMergeable, Mergeable newMergeable)
        {
            MergeCustomActions(oldMergeable, newMergeable);
            
            OnMerged?.Invoke(oldMergeable, newMergeable);
            
            _mergedActionExecutor.Execute();
        }
    }
}