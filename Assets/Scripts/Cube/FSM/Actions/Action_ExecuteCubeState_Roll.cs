namespace BLUE.ACubeStory.Cube
{
    public class Action_ExecuteCubeState_Roll : Action_ExecuteCubeStateBase<FSMTransitionMessage_Cube_Roll>
    {
        protected override ECubeState GetTargetState()
        {
            return ECubeState.Roll;
        }
    }
}