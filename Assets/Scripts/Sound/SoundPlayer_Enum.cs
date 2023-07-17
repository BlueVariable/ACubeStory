using System;
using BLUE.SoundSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Sound
{
    [Serializable]
    public class SoundPlayData_Enum : SoundPlayData
    {
        [field: SerializeField] public ESound SoundType { get; private set; }

        public SoundPlayData_Enum(ESound soundType)
        {
            SoundType = soundType;
        }
    } 
    
    [Serializable]
    public class SoundPlayer_Enum : SoundPlayerBase<SoundPlayData_Enum>
    {
        public override void Play(SoundPlayData_Enum playData)
        {
            
        }
    }
}