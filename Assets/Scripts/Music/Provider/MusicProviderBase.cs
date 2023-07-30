using UnityEngine;

namespace BLUE.ACubeStory.Music
{
    public abstract class MusicProviderBase : MonoBehaviour
    {
        public abstract EMusic GetMusicType();
        public abstract AudioSource GetAudioSource();
    }
}