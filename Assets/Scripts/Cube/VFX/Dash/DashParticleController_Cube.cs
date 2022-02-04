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

        [SerializeField] private float _particleTriggerDistance = 0.5f;
        
        [SerializeField] private float _particleDistanceFromCube = 0.625f;
        
        [SerializeField] private float _particleSize = 0.125f;

        private IEnumerator _particleTriggerRoutine;
        
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
            StartParticleTriggerRoutine(direction);
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

        private void StartParticleTriggerRoutine(Vector3 direction)
        {
            StopParticleTriggerRoutine();

            _particleTriggerRoutine = ParticleTriggerProgress(direction);

            StartCoroutine(_particleTriggerRoutine);
        }

        private void StopParticleTriggerRoutine()
        {
            if (_particleTriggerRoutine != null)
            {
                StopCoroutine(_particleTriggerRoutine);
            }
        }

        private IEnumerator ParticleTriggerProgress(Vector3 direction)
        {
            Vector3 initialPos = transform.position;

            while (Vector3.Distance(initialPos, transform.position) < _particleTriggerDistance)
            {
                yield return null;
            }
            
            PlayDashParticle(direction);
        }
        
        private void PlayDashParticle(Vector3 direction)
        {
            PoolObject poolObject = _particlePool.AllocatePoolObject();

            DashParticle_Cube dashParticle = poolObject.GetComponent<DashParticle_Cube>();

            dashParticle.transform.position = transform.position + GetParticlePositionOffset(direction);

            dashParticle.transform.localScale = GetParticleScale(direction);
            
            dashParticle.DashVFX.Play();

            _lastAllocatedPoolObject = poolObject;
        }
    }
}
