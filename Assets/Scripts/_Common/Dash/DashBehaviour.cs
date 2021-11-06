using System;
using UnityEngine;

namespace _Common.Dash
{
    [Serializable] 
    public class DashSettings
    {
        [SerializeField] private float _dashDistance = 0;
        public float DashDistance => _dashDistance;

        [SerializeField] private float _dashDuration = 0;
        public float DashDuration => _dashDuration;

        public DashSettings(float dashDistance, float dashDuration)
        {
            _dashDistance = dashDistance;
            _dashDuration = dashDuration;
        }
    }
    
    public class DashBehaviour : MonoBehaviour
    {
        public void Dash(Vector2 dashDirection, Action onDashCompletedCallback = null)
        {
            
        }
    }
}