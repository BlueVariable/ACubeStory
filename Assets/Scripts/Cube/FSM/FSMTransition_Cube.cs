using BLUE.FSMSystem;

namespace BLUE.ACubeStory.Cube
{
    public class FSMTransition_Cube : FSMTransition<ECubeState>
    {
        public FSMTransition_Cube(ECubeState initialStateType, ECubeState targetStateType) 
            : base(initialStateType, targetStateType)
        {
        }
    }
}