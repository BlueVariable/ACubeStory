using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public abstract class MergeableSlotAssignerBase : MonoBehaviour
    {
        public abstract bool TryAssignSlots(MergeableSlot[] slots);
        public abstract bool TryUnassignSlots(MergeableSlot[] slots);

        public bool UnassignAllSlots(MergeableSlot[] slots)
        {
            bool unAssigned = false;
            
            foreach (MergeableSlot slot in slots)
            {
                if (slot.TryUnassignMergeable())
                {
                    unAssigned = true;
                }
            }

            return unAssigned;
        }
    }
}