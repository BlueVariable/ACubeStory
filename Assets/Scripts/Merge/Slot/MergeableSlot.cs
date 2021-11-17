using System;
using System.Linq;
using BLUE.ActionSystem;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class MergeableSlot : MonoBehaviour
    {
        [SerializeField] private ActionExecutor _mergeableAssignedActionExecutor = null;
        [SerializeField] private ActionExecutor _mergeableUnassignedActionExecutor = null;

        [SerializeField] private SimpleConditionBase[] _mergeableAssignConditions = default;
        [SerializeField] private SimpleConditionBase[] _mergeableUnassignConditions = default;
        
        public bool IsAssigned { get; private set; }
        private Mergeable _assignedMergeable;

        #region Events

        public Action<Mergeable, MergeableSlot> OnAssigned { get; set; }
        public Action<Mergeable, MergeableSlot> OnUnassigned { get; set; }

        #endregion

        public bool TryAssignMergeable(Mergeable mergeable)
        {
            if (!_mergeableAssignConditions.All(val => val.IsConditionSatisfied()))
            {
                return false;
            }

            if (_assignedMergeable != null)
            {
                if (!TryUnassignMergeable())
                {
                    return false;
                }
            }
            
            _assignedMergeable = mergeable;

            IsAssigned = true;
            
            _mergeableAssignedActionExecutor.Execute();

            OnAssigned?.Invoke(_assignedMergeable, this);

            return true;
        }

        public bool TryUnassignMergeable()
        {
            if (_assignedMergeable == null)
            {
                return false;
            }

            if (!_mergeableUnassignConditions.All(val => val.IsConditionSatisfied()))
            {
                return false;
            }

            IsAssigned = false;

            OnUnassigned?.Invoke(_assignedMergeable, this);
            
            _assignedMergeable = null;
            
            _mergeableUnassignedActionExecutor.Execute();

            return true;
        }
    }
}
