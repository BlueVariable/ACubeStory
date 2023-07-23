using BLUE.SoundSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Sound
{
    public class SoundRequestor_TypeBased : SoundRequestorBase<ESoundRequestor>
    {
        [field: SerializeField] public ESoundRequestor SoundRequestorType { get; private set; } 
            = ESoundRequestor.None; 
        
        public override ESoundRequestor GetSoundRequestorType()
        {
            return SoundRequestorType;
        }
    }
}