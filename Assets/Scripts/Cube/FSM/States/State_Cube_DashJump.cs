using BLUE.FSMSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class State_Cube_DashJump : StateBase<ECubeState, FSMTransitionMessage_Cube_Dash>
    {
        [SerializeField] private 
        
        public override ECubeState GetStateType()
        {
            return ECubeState.Dash_Jump;
        }
        
        
    }
}