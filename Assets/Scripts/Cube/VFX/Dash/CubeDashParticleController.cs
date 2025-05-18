using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeDashParticleController : MonoBehaviour
    {
        #region Constants

        private const float LOCAL_POS_OFFSET_MULTIPLIER = 0.5f;

        #endregion
        
        [SerializeField] private State_Cube_Dash _dashState = null;
        
        [SerializeField] private ParticleSystem _dashVFX = null;

        
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
            _dashState.OnEnteringDashState += OnDashStarted;
            _dashState.OnDashCompleted += OnDashCompleted;
            _dashState.OnExitingDashState += OnDashCompleted;
        }

        private void UnregisterFromDashState()
        {
            _dashState.OnEnteringDashState -= OnDashStarted;
            _dashState.OnDashCompleted -= OnDashCompleted;
            _dashState.OnExitingDashState -= OnDashCompleted;
        }
        
        private void OnDashStarted(Vector3 direction)
        {
            PlayDashParticle();
        }
        
        private void OnDashCompleted()
        {
            StopDashParticle();
        }

        private void PlayDashParticle()
        {
            Vector3 dashDirection = _dashState.LastCandidateTransitionMessage.DashDirection;
            
            _dashVFX.transform.localPosition = Vector3.zero;
            
            _dashVFX.transform.position += dashDirection.normalized * - LOCAL_POS_OFFSET_MULTIPLIER;

            _dashVFX.transform.LookAt(Cube.Instance.GetPosition());
            
            _dashVFX.Play();
        }
        
        private void StopDashParticle()
        {
            _dashVFX.Stop();
        }
    }
}
