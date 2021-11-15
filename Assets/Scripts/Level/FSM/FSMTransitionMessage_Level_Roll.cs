using UnityEngine;

namespace BLUE.ACubeStory.Level
{
    public class FSMTransitionMessage_Level_Roll : FSMTransitionMessage_Level
    {
        public Vector3 Direction { get; }

        public FSMTransitionMessage_Level_Roll(Vector3 direction)
        {
            Direction = direction;
        }
    }
}