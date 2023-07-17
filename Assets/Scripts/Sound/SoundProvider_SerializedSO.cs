using System;
using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.Sound
{
    public class SoundProvider_SerializedSO : MonoBehaviour
    {
        [SerializeField] private SettingsSO_Sound[] _commonSoundSettings = null;
        [SerializeField] private SettingsSO_Sound[] _otherSoundSettings = null;

        private Dictionary<ESound, SoundSetting> _soundSettingMap =
            new Dictionary<ESound, SoundSetting>();

        private bool _isInited = false;
        
        public bool TryGetSoundSettings(
            ESound sound,
            out SoundSetting soundSetting)
        {
            if (!_isInited)
            {
                InitSoundMap();
            }

            if (!_soundSettingMap.TryGetValue(sound, out soundSetting))
            {
                Debug.Log("<color=magenta>SoundManager: Requested sound </color> " + sound + " not found!");

                return false;
            }

            return true;
        }

        private void InitSoundMap()
        {
            _soundSettingMap = new Dictionary<ESound, SoundSetting>();
            
            foreach (SettingsSO_Sound settingsSO in _commonSoundSettings)
            {
                foreach (SoundSetting setting in settingsSO.SoundSettings)
                {
                    _soundSettingMap[setting.SoundType] = setting;
                }
            }
            
            foreach (SettingsSO_Sound settingsSO in _otherSoundSettings)
            {
                foreach (SoundSetting setting in settingsSO.SoundSettings)
                {
                    _soundSettingMap[setting.SoundType] = setting;
                }
            }
            
            _isInited = true;
        }
    }
}