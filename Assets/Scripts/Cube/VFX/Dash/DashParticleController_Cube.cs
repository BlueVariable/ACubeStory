using BLUE.PoolingSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class DashParticleController_Cube : MonoBehaviour
    {
        [SerializeField] private State_Cube_Dash _dashState = null;

        [SerializeField] private Pool_MB _particlePool = null;
        
        [SerializeField] private float _particleDistanceFromCube = 0.625f;
        
        [SerializeField] private float _particleSize = 0.125f;

        private PoolObject _lastAllocatedPoolObject = null;
        
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
            PoolObject poolObject = _particlePool.AllocatePoolObject();

            DashParticle_Cube dashParticle = poolObject.GetComponent<DashParticle_Cube>();

            dashParticle.transform.position = transform.position + GetParticlePositionOffset(direction);

            dashParticle.transform.localScale = GetParticleScale(direction);
            
            dashParticle.DashVFX.Play();

            _lastAllocatedPoolObject = poolObject;
        }
        
        private void OnDashCompleted()
        {
            if (_lastAllocatedPoolObject != null)
            {
                DashParticle_Cube dashParticle = _lastAllocatedPoolObject.GetComponent<DashParticle_Cube>();

                dashParticle.DashVFX.Stop();
            }
        }

        private Vector3 GetParticlePositionOffset(Vector3 dashDirection)
        {
            Vector3 offsetPos = -_particleDistanceFromCube * new Vector3(dashDirection.x, 0, dashDirection.z);

            return offsetPos;
        }

        private Vector3 GetParticleScale(Vector3 dashDirection)
        {
            Vector3 particleSize = Vector3.one;

            if (Mathf.Abs(dashDirection.x) > 0)
            {
                particleSize.x = _particleSize;
            }
            else if (Mathf.Abs(dashDirection.z) > 0)
            {
                particleSize.z = _particleSize;
            }

            return particleSize;
        }
    }
}
