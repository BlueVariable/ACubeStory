using BLUE.Common;

namespace BLUE.ACubeStory.Moth
{
    public class Condition_IsChildOfAscendableMothPack : SimpleConditionBase
    {
        public override bool IsConditionSatisfied()
        {
            return GetComponentInParent<MothPackAscendingController>() != null;
        }
    }
}