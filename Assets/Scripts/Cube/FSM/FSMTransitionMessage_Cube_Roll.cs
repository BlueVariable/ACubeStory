using BLUE.ACubeStory.Common;

namespace BLUE.ACubeStory.Cube
{
    public class FSMTransitionMessage_Cube_Roll : FSMTransitionMessage_Cube
    {
        public RollBehaviourBase.ERollDirection Direction { get; }

        public FSMTransitionMessage_Cube_Roll(RollBehaviourBase.ERollDirection direction)
        {
            Direction = direction;
        }
    }
}