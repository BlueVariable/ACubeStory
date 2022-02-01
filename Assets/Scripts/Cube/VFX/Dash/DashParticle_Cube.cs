using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    [RequireComponent(typeof(ParticleSystem))]
    public class DashParticle_Cube : MonoBehaviour
    {
        private ParticleSystem _dashVFX;
        public ParticleSystem DashVFX
        {
            get
            {
                if (_dashVFX == null)
                {
                    _dashVFX = GetComponent<ParticleSystem>();
                }

                return _dashVFX;
            }
        }
    }
}