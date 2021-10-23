using System.Collections.Generic;
using BLUE.FSMSystem;

namespace BLUE.ACubeStory.Cube
{
    public class FSM_Cube : FSMBase<ECubeState, FSMTransitionMessage_Cube, FSMTransition_Cube>
    {
        protected override List<FSMTransition_Cube> GetInitialPossibleTransitions()
        {
            return new List<FSMTransition_Cube>
            {
                new FSMTransition_Cube(ECubeState.None, ECubeState.Idle),
                
                new FSMTransition_Cube(ECubeState.Idle, ECubeState.Roll),
                new FSMTransition_Cube(ECubeState.Idle, ECubeState.Jump),
                new FSMTransition_Cube(ECubeState.Idle, ECubeState.Fall),
                new FSMTransition_Cube(ECubeState.Idle, ECubeState.Climb),
                new FSMTransition_Cube(ECubeState.Idle, ECubeState.Descend),
                
                new FSMTransition_Cube(ECubeState.Roll, ECubeState.Dash_Roll),
                new FSMTransition_Cube(ECubeState.Roll, ECubeState.Idle),
                
                new FSMTransition_Cube(ECubeState.Jump, ECubeState.Dash_Jump),
                new FSMTransition_Cube(ECubeState.Jump, ECubeState.Fall),
                
                new FSMTransition_Cube(ECubeState.Fall, ECubeState.Idle),
                
                new FSMTransition_Cube(ECubeState.Dash_Roll, ECubeState.Idle),
                
                new FSMTransition_Cube(ECubeState.Dash_Jump, ECubeState.Fall),
                
                new FSMTransition_Cube(ECubeState.Climb, ECubeState.Idle),
                
                new FSMTransition_Cube(ECubeState.Descend, ECubeState.Idle),
            };
        }
    }
}