using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class DashParticleController_Cube : MonoBehaviour
    {
        [SerializeField] private Transform _cubeVisualReference = null;

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
        }

        private void OnDestroy()
        {
            UnregisterFromDashState();
        }
        
        private void RegisterToDashState()
        {
            _dashState.OnDashStarted += OnDashStarted;
            _dashState.OnDashCompleted += OnDashCompleted;
        }

        private void UnregisterFromDashState()
        {
            _dashState.OnDashStarted -= OnDashStarted;
            _dashState.OnDashCompleted -= OnDashCompleted;
        }
        
        private void OnDashStarted(Vector3 direction)
        {
            if (_DashParticle == null)
            {
                return;
            }
            
            Vector3 offsetPos = -_dashParticleDistance * new Vector3(direction.x, 0, direction.z);
            
            _DashParticle.transform.position =  _cubeVisualReference.position + offsetPos;

            _DashParticle.Play();
        }
        
        private void OnDashCompleted()
        {
            _DashParticle.Stop();
        }
    }
}