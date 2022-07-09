using BLUE.Common;

namespace BLUE.ACubeStory.Moth
{
    public class Condition_ParentMothPackAscendingCompleted : SimpleConditionBase
    {
        public override bool IsConditionSatisfied()
        {
            MothPackAscendingController mothPackAscendingController =
                GetComponentInParent<MothPackAscendingController>();

            if (mothPackAscendingController == null)
            {
                return true;
            }

            return mothPackAscendingController.IsCompleted();
        }
    }
}