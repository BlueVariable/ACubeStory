using BLUE.Helpers;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class DashParticleController_Cube : MonoBehaviour
    {
        [SerializeField] private Transform _cubeVisualReference = null;
        
        [SerializeField] private FSM_Cube _cubeFSM = null;

        [SerializeField] private State_Cube_Dash _dashState = null;
        
        [SerializeField] private float _dashParticleDistance = 0.5f;
        
        private ParticleSystem _dashParticle;

        private ParticleSystem _DashParticle
        {
            get
            {
                if (_dashParticle == null)
                {
                    _dashParticle = GetComponentInChildren<DashParticle_Cube>().DashVFX;
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
            
            Vector3 offsetPos = -_dashParticleDistance * new Vector3(direction.x, 0, direction.z);
            
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