using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    [Serializable]
    public class FSMTransitionMessage_Cube_Float : FSMTransitionMessage_Cube
    {
        [SerializeField] private Vector3 _direction = default;
        public Vector3 Direction => _direction;

        public FSMTransitionMessage_Cube_Float(Vector3 direction)
        {
            _direction = direction;
        }
    }
}