using System;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    [Serializable]
    public class FSMTransitionMessage_Cube_Roll : FSMTransitionMessage_Cube
    {
        [SerializeField] private Vector3 _direction = default;
        public Vector3 Direction => _direction;

        [SerializeField] private float _speed = default;
        public float Speed => _speed;

        public FSMTransitionMessage_Cube_Roll(Vector3 direction, float speed = 0f)
        {
            _direction = direction;
            _speed = speed;
        }
    }
}