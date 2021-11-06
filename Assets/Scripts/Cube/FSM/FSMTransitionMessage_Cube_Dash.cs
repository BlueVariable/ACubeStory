using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class FSMTransitionMessage_Cube_Dash : FSMTransitionMessage_Cube
    {
        public Vector3 DashDirection { get; }

        public FSMTransitionMessage_Cube_Dash(Vector3 dashDirection)
        {
            DashDirection = dashDirection;
        }
    }
}