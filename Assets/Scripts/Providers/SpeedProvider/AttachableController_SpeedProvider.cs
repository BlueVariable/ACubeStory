using BLUE.AttachableSystem;

namespace BLUE.ACubeStory.Behaviours
{
    public class AttachableController_SpeedProvider : 
        AttachableControllerBase<SpeedProviderBase>,
        ISpeedProvider
    {
        public float GetSpeed()
        {
            return _attachedAttachables[0].GetSpeed();
        }
    }
}