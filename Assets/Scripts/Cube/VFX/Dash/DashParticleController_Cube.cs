using System;
using BLUE.Helpers;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class DashParticleController_Cube : MonoBehaviour
    {
        [SerializeField] private Transform _cubeVisualReference = null;
        
        [SerializeField] private FSM_Cube _cubeFSM = null;

        [SerializeField] private State_Cube_Dash _dashState = null;

        [SerializeField] private float _particleDistanceFromCube = 0.625f;
        
        [SerializeField] private float _particleSize = 0.125f;
        
        private ParticleSystem _dashParticle;

        private ParticleSystem _DashParticle
        {
            get
            {
                DashParticle_Cube cubeDashParticle = GetComponentInChildren<DashParticle_Cube>();
                
                if (_dashParticle == null && cubeDashParticle != null)
                {
                    _dashParticle = cubeDashParticle.DashVFX;
                }

                return _dashParticle;
            }
        }

        private void Awake()
        {
            RegisterToDashState();

            RegisterToCubeFSM();
        }

        private void OnDestroy()
        {
            UnregisterFromDashState();
            
            UnregisterFromCubeFSM();
        }
        
        private void RegisterToDashState()
        {
            _dashState.OnEnteredDashState += OnEnteredDashState;
        }

        private void UnregisterFromDashState()
        {
            _dashState.OnEnteredDashState -= OnEnteredDashState;
        }

        private void RegisterToCubeFSM()
        {
            if (_DashParticle == null)
            {
                return;
            }
            
            _cubeFSM.OnStateExited += OnStateExited;
        }

        private void UnregisterFromCubeFSM()
        {
            _cubeFSM.OnStateEntered -= OnStateExited;
        }

        private void OnEnteredDashState(Vector3 direction)
        {
            if (_DashParticle == null)
            {
                return;
            }
            
            Vector3 offsetPos = -_particleDistanceFromCube * new Vector3(direction.x, 0, direction.z);

            Vector3 particleSize = Vector3.one;

            if (Mathf.Abs(direction.x) > 0)
            {
                particleSize.x = _particleSize;
            }
            else if (Mathf.Abs(direction.z) > 0)
            {
                particleSize.z = _particleSize;
            }

            _DashParticle.transform.localScale = particleSize;
            
            _DashParticle.transform.position =  _cubeVisualReference.position + offsetPos;

            _DashParticle.Play();
        }

        private void OnStateExited(ECubeState eCubeState)
        {
            if (eCubeState != ECubeState.Dash)
            {
                return;
            }
            
            _DashParticle.Stop();
        }
    }
}