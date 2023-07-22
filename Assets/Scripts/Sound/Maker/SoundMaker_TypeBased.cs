using BLUE.SoundSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Sound
{
    public class SoundMaker_TypeBased : SoundMakerBase<ESoundMaker>
    {
        [field: SerializeField] public ESoundMaker SoundMakerType { get; private set; } = ESoundMaker.None; 
        
        public override ESoundMaker GetSoundMakerType()
        {
            return SoundMakerType;
        }
    }
}