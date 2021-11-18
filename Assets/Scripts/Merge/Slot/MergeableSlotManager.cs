using System;
using System.Linq;
using BLUE.ActionSystem;
using BLUE.TriggerSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class MergeableSlotManager : MonoBehaviour
    {
        [SerializeField] private TriggerBase[] _tryAssignRequestors = null;
        [SerializeField] private TriggerBase[] _tryUnassignRequestors = null;
        [SerializeField] private TriggerBase[] _unassignAllRequestors = null;

        [SerializeField] private ActionExecutor _allSlotsAssignedActionExecutor = null;
        
        [SerializeField] private MergeableSlotAssignerBase _slotAssigner = null;

        private MergeableSlot[] _mergeableSlots;
        private MergeableSlot[] _MergeableSlots
        {
            get
            {
                if (_mergeableSlots == null)
                {
                    _mergeableSlots = FindObjectsOfType<MergeableSlot>();
                }

                return _mergeableSlots;
            }
        }

        #region Events

        public Action OnAllSlotsAssigned { get; set; }
        public Action<Mergeable, MergeableSlot> OnSlotAssigned { get; set; }
        public Action<Mergeable, MergeableSlot> OnSlotUnassigned { get; set; }

        #endregion

        private void Awake()
        {
            RegisterToRequestors();
            
            RegisterToMergeableSlots();
        }

        private void OnDestroy()
        {
            UnregisterFromRequestors();
            
            UnregisterFromMergeableSlots();
        }

        private void RegisterToRequestors()
        {
            foreach (TriggerBase assignRequestor in _tryAssignRequestors)
            {
                assignRequestor.OnTriggered += OnAssignRequested;
            }
            
            foreach (TriggerBase unassignRequestor in _tryUnassignRequestors)
            {
                unassignRequestor.OnTriggered += OnUnassignRequested;
            }
            
            foreach (TriggerBase unassignAllRequestor in _unassignAllRequestors)
            {
                unassignAllRequestor.OnTriggered += OnUnassignAllRequested;
            }
        }
        
        private void UnregisterFromRequestors()
        {
            foreach (TriggerBase assignRequestor in _tryAssignRequestors)
            {
                assignRequestor.OnTriggered -= OnAssignRequested;
            }
            
            foreach (TriggerBase unassignRequestor in _tryUnassignRequestors)
            {
                unassignRequestor.OnTriggered -= OnUnassignRequested;
            }
            
            foreach (TriggerBase unassignAllRequestor in _unassignAllRequestors)
            {
                unassignAllRequestor.OnTriggered -= OnUnassignAllRequested;
            }
        }

        private void OnAssignRequested(TriggerBase trigger)
        {
            _slotAssigner.TryAssignSlots(_MergeableSlots);
        }

        private void OnUnassignRequested(TriggerBase trigger)
        {
            _slotAssigner.TryUnassignSlots(_MergeableSlots);
        }
        
        private void OnUnassignAllRequested(TriggerBase trigger)
        {
            _slotAssigner.UnassignAllSlots(_MergeableSlots);
        }

        private void RegisterToMergeableSlots()
        {
            foreach (MergeableSlot slot in _MergeableSlots)
            {
                slot.OnAssigned += OnAssigned;
                slot.OnUnassigned += OnUnassigned;
            }
        }
        
        private void UnregisterFromMergeableSlots()
        {
            foreach (MergeableSlot slot in _MergeableSlots)
            {
                slot.OnAssigned -= OnAssigned;
                slot.OnUnassigned -= OnUnassigned;
            }
        }

        private void OnAssigned(Mergeable assignedMergeable, MergeableSlot slot)
        {
            OnSlotAssigned?.Invoke(assignedMergeable, slot);
            
            ProcessSlotAssignment();
        }

        private void OnUnassigned(Mergeable unassignedMergeable, MergeableSlot slot)
        {
            OnSlotUnassigned?.Invoke(unassignedMergeable, slot);
        }

        private void ProcessSlotAssignment()
        {
            if (_MergeableSlots.All(val => val.IsAssigned))
            {
                OnAllSlotsAssigned?.Invoke();

                _allSlotsAssignedActionExecutor.Execute();
            }
        }
    }
}