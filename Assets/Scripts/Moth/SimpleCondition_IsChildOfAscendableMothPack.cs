using BLUE.Common;

namespace BLUE.ACubeStory.Moth
{
    public class SimpleCondition_IsChildOfAscendableMothPack : SimpleConditionBase
    {
        public override bool IsConditionSatisfied()
        {
            return GetComponentInParent<MothPackAscendable>() != null;
        }
    }
}