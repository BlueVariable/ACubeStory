using System;
using System.Collections.Generic;
using BLUE.PoolingSystem;
using BLUE.SoundSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Sound
{
    public class SoundManager : MonoBehaviour
    {
        #region Singleton

        private static SoundManager _instance;
        public static SoundManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SoundManager>();
                }

                return _instance;
            }
        }

        #endregion

        private bool _isInited = false;
        
        private Dictionary<ESound, Pool_Sound> _soundPools = null;
        private Dictionary<ESound, Pool_Sound> _SoundPools
        {
            get
            {
                if (!_isInited)
                {
                    InitPools();
                }

                return _soundPools;
            }
        }

        private readonly Dictionary<SoundPlayerBase, PoolObject> _allocatedSoundPlayerMap = new();

        public bool AllocateSoundPlayer(
            ESound sound,
            Action<SoundPlayerBase> setter)
        {
            if(!_SoundPools.TryGetValue(sound, out Pool_Sound pool))
            {
                Debug.Log($"<color=orange> Couldn't find a pool for: </color>{sound}");
                
                return false;
            }

            PoolObject poolObject = pool.AllocatePoolObject();

            if (!poolObject.TryGetComponent(out SoundPlayerBase soundPlayer))
            {
                return false;
            }
            
            _allocatedSoundPlayerMap.Add(soundPlayer, poolObject);
            
            setter?.Invoke(soundPlayer);
            
            return true;
        }
        
        public void DeallocateSoundPlayer(
            SoundPlayerBase soundPlayer)
        {
            if (!_allocatedSoundPlayerMap.TryGetValue(soundPlayer, out PoolObject poolObject))
            {
                return;
            }

            if (soundPlayer.IsPlaying)
            {
                soundPlayer.Stop();
            }

            _allocatedSoundPlayerMap.Remove(soundPlayer);
            
            poolObject.Deallocate();
        }

        private void InitPools()
        {
            _soundPools = new Dictionary<ESound, Pool_Sound>();
                    
            Pool_Sound[] pools = GetComponentsInChildren<Pool_Sound>();

            foreach (Pool_Sound pool in pools)
            {
                _soundPools[pool.SoundType] = pool;
            }

            _isInited = true;
        }
    }
}