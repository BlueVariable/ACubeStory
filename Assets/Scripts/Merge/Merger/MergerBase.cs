using System;
using System.Collections.Generic;
using System.Linq;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public abstract class MergerBase : MonoBehaviour
    {
        [SerializeField] private Transform _mergerRoot = null;
        
        [SerializeField] private ActionExecutor _mergedActionExecutor = null;
        
        [SerializeField] private ActionExecutor _unmergedActionExecutor = null;

        [SerializeField] private MergeConditionBase[] _mergeConditions = new MergeConditionBase[0];
        
        [SerializeField] private UnmergeConditionBase[] _unmergeConditions = new UnmergeConditionBase[0];

        #region Events

        public Action<Mergeable, Mergeable> OnMerged { get; set; }
        
        public Action<Mergeable> OnUnmerged { get; set; }

        #endregion
        
        protected abstract HashSet<Mergeable> GetCandidateMergeables(Mergeable formerMergeable);

        protected virtual void MergeCustomActions(Mergeable formerMergeable, Mergeable mergedMergeable) { }
        
        protected virtual void UnmergeCustomActions(Mergeable mergeable) { }

        public bool TryCollectiveMerge(HashSet<Mergeable> formerMergeables, out HashSet<Mergeable> mergedMergeables)
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
        
        public bool TryMerge(HashSet<Mergeable> formerMergeables, Mergeable targetMergeable)
        {
            if (targetMergeable.IsMerged)
            {
                return false;
            }
            
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

                        return true;
                    }
                }
            }

            return false;
        }
        
        public bool TryUnmerge(HashSet<Mergeable> formerMergeables, Mergeable targetMergeable)
        {
            if (!formerMergeables.Contains(targetMergeable))
            {
                return false;
            }

            if (!targetMergeable.IsMerged)
            {
                return false;
            }
            
            if (_unmergeConditions.All(val 
                => val.CanUnmerge(targetMergeable)))
            {
                Unmerge(targetMergeable);

                return true;
            }

            return false;
        }

        private void Merge(Mergeable formerMergeable, Mergeable mergedMergeable)
        {
            mergedMergeable.transform.parent = _mergerRoot;
            
            MergeCustomActions(formerMergeable, mergedMergeable);
            
            OnMerged?.Invoke(formerMergeable, mergedMergeable);

            _mergedActionExecutor.Execute();
            
            mergedMergeable.Merged();
        }

        private void Unmerge(Mergeable mergeable)
        {
            mergeable.transform.parent = null;

            UnmergeCustomActions(mergeable);

            OnUnmerged?.Invoke(mergeable);
            
            _unmergedActionExecutor.Execute();
            
            mergeable.Unmerged();
        }
    }
}