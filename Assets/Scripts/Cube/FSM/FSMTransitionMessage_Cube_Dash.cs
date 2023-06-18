using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    [Serializable]
    public class FSMTransitionMessage_Cube_Dash : FSMTransitionMessage_Cube
    {
        [SerializeField] private Vector3 _dashDirection = default;
        public Vector3 DashDirection => _dashDirection;

        public FSMTransitionMessage_Cube_Dash(Vector3 dashDirection)
        {
            _dashDirection = dashDirection;
        }
    }
}