using UnityEngine;

namespace BLUE.ACubeStory.Music
{
    public class MusicProvider_Serialized : MusicProviderBase
    {
        [SerializeField] private EMusic _musicType = EMusic.None;
            
        [SerializeField] private AudioSource _audioSource = null;

        public override EMusic GetMusicType()
        {
            return _musicType;
        }

        public override AudioSource GetAudioSource()
        {
            return _audioSource;
        }
    }
}