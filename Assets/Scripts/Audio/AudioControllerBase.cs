using UnityEngine;

namespace BLUE.ACubeStory.Audio
{
    public abstract class AudioControllerBase : MonoBehaviour
    {
        public bool CanPlayAudio { get; private set; } = true;
        
        public abstract EAudioController GetAudioControllerType();

        public void SetCanPlayAudio(bool canPlayAudio)
        {
            CanPlayAudio = canPlayAudio;
        }
    }
}