using UnityEngine;

namespace BLUE.ACubeStory.Sound
{ 
        [CreateAssetMenu(fileName = "SettingsSO_Sound", menuName = "BLUE/Settings/Sound/Create SettingsSO_Sound")]
        public class SettingsSO_Sound : ScriptableObject
        {
                [field: SerializeField] public SoundSetting[] SoundSettings { get; private set; } = new SoundSetting[]{};
        }
}