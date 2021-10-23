using BLUE.FSMSystem;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_Idle : StateBase<ECubeState, FSMTransitionMessage_Cube>
    {
        public override ECubeState GetStateType()
        {
            return ECubeState.Idle;
        }
    }
}