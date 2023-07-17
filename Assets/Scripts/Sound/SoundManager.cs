using System;
using UnityEngine;

namespace BLUE.ACubeStory.Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private Transform _soundProviderObj = null;

        private ISoundSettingProvider _soundSettingProvider;
        private ISoundSettingProvider SoundSettingProvider
        {
            get
            {
                if (_soundSettingProvider == null)
                {
                    _soundSettingProvider = _soundProviderObj
                        .GetComponent<ISoundSettingProvider>();
                }

                return _soundSettingProvider;
            }
        }
        
        public bool GetSoundClip(
            ESound sound,
            Action<AudioClip> setter)
        {
            bool result = SoundSettingProvider.TryGetSoundSettings(sound, out SoundSetting setting);
            
            setter?.Invoke(setting.SoundClip);

            return result;
        }
    }
}