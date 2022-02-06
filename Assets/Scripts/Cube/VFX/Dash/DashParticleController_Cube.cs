using UnityEngine;
using BLUE.Helpers;
using BLUE.PoolingSystem;
using System.Collections;

namespace BLUE.ACubeStory.Cube
{
    public class DashParticleController_Cube : MonoBehaviour
    {
        [SerializeField] private State_Cube_Dash _dashState = null;

        [SerializeField] private Pool_MB _particlePool = null;

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
            PlayDashParticle(direction);
        }
        
        private void OnDashCompleted()
        {
            if (_lastAllocatedPoolObject != null)
            {
                PoolObject dashParticlePoolObject = _lastAllocatedPoolObject;
                   
                DashParticle_Cube dashParticle = _lastAllocatedPoolObject.GetComponent<DashParticle_Cube>();

                dashParticle.DashVFX.Stop();

                CoroutineHelper.Instance.WaitForSeconds(dashParticle.DashVFX.main.duration, () =>
                {
                    _particlePool.DeallocatePoolObject(dashParticlePoolObject);
                });
            }
        }

        private void PlayDashParticle(Vector3 direction)
        {
            PoolObject poolObject = _particlePool.AllocatePoolObject();

            DashParticle_Cube dashParticle = poolObject.GetComponent<DashParticle_Cube>();
            
            dashParticle.transform.localPosition = Vector3.zero;

            dashParticle.DashVFX.Play();

            _lastAllocatedPoolObject = poolObject;
        }
    }
}
