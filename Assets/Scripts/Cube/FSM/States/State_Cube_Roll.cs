using BLUE.FSMSystem;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Roll : StateBase<ECubeState, FSMTransitionMessage_Cube>
    {
        public override ECubeState GetStateType()
        {
            return ECubeState.Roll;
        }
    }
}