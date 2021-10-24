using BLUE.AttachableSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Behaviours
{
    public class AttachableController_RollPointProvider : 
        AttachableControllerBase<RollPointProviderBase>,
        IRollPointProvider
    {
        public Vector3 GetRollPoint(RollBehaviourBase.ERollDirection direction)
        {
            return _attachedAttachables[0].GetRollPoint(direction);
        }
    }
}