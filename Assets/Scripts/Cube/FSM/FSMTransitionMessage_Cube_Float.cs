using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class FSMTransitionMessage_Cube_Float : FSMTransitionMessage_Cube
    {
        public Vector3 Direction { get; }

        public FSMTransitionMessage_Cube_Float(Vector3 direction)
        {
            Direction = direction;
        }
    }
}