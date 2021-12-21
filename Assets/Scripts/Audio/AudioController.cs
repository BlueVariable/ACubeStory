using UnityEngine;

namespace BLUE.ACubeStory.Audio
{
    public abstract class AudioControllerBase : MonoBehaviour
    {
        public abstract EAudioController GetAudioControllerType();
    }
}