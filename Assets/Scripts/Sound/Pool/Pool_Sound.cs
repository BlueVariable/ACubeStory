using BLUE.PoolingSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Sound
{
    public class Pool_Sound : Pool<PoolObject_Sound>
    {
        [field: SerializeField] public ESound SoundType { get; private set; } = ESound.None;
        
        [SerializeField] private PoolSettings _poolSettings = null;
        
        [SerializeField] private bool _initOnAwake = false;

        private void Awake()
        {
            if (_initOnAwake)
            {
                InitPool(_poolSettings);
            }
        }
    }
}