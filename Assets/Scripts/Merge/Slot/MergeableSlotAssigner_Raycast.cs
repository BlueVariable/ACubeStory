using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class MergeableSlotAssigner_Raycast : MergeableSlotAssignerBase
    {
        [SerializeField] private float _rayDistance = 1f;

        [SerializeField] private LayerMask _targetMask = default;

        public override bool TryAssignSlots(MergeableSlot[] slots)
        {
            bool assigned = false;
            
            foreach(MergeableSlot mergeableSlot in slots)
            {
                if (!Physics.Raycast(mergeableSlot.transform.position, Vector3.up,
                    out RaycastHit hit, _rayDistance, _targetMask))
                {
                    continue;
                }

                Mergeable mergeable = hit.collider.GetComponentInParent<Mergeable>();

                if (mergeable == null)
                {
                    continue;
                }

                if (mergeableSlot.TryAssignMergeable(mergeable))
                {
                    assigned = true;
                }
            }

            return assigned;
        }

        public override bool TryUnassignSlots(MergeableSlot[] slots)
        {
            bool unassigned = false;
            
            foreach(MergeableSlot mergeableSlot in slots)
            {
                if (!Physics.Raycast(mergeableSlot.transform.position, Vector3.up,
                    out RaycastHit hit, _rayDistance, _targetMask))
                {
                    if (mergeableSlot.TryUnassignMergeable())
                    {
                        unassigned = true;
                    }
                }
            }

            return unassigned;
        }
    }
}
