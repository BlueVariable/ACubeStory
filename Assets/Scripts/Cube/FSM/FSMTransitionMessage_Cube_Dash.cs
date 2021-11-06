using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class FSMTransitionMessage_Cube_Dash : FSMTransitionMessage_Cube
    {
        public Vector2 DashDirection { get; }

        public FSMTransitionMessage_Cube_Dash(Vector2 dashDirection)
        {
            DashDirection = dashDirection;
        }
    }
}