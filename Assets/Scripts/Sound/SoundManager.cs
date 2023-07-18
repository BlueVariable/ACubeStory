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
        
        private Dictionary<ESound, Pool_Sound> _soundPools = null;

        private Dictionary<ESound, Pool_Sound> _SoundPools
        {
            get
            {
                if (_soundPools == null)
                {
                    InitPools();
                }

                return _soundPools;
            }
        } 
        
        public bool GetSoundPlayer(
            ESound sound,
            Action<SoundPlayerBase> setter)
        {
            if(!_soundPools.TryGetValue(sound, out Pool_Sound pool))
            {
                return false;
            }

            PoolObject poolObject = pool.AllocatePoolObject();

            if (!poolObject.TryGetComponent(out SoundPlayerBase soundPlayer))
            {
                return false;
            }
            
            setter?.Invoke(soundPlayer);
            
            return true;
        }

        private void InitPools()
        {
            _soundPools = new Dictionary<ESound, Pool_Sound>();
                    
            Pool_Sound[] pools = GetComponentsInChildren<Pool_Sound>();

            foreach (Pool_Sound pool in pools)
            {
                _soundPools[pool.SoundType] = pool;
            }
        }
    }
}