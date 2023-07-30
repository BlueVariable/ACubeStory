using System;
using BLUE.SoundSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Music
{
    [Serializable]
    public class SoundPlayData_Music : SoundPlayData
    {
        [field: SerializeField] public EMusic MusicType { get; private set; } = EMusic.None;
        [field: SerializeField] public float BlendDuration { get; private set; } = 5f;

        public SoundPlayData_Music(
            EMusic musicType,
            float blendDuration = 5f,
            bool loop = false) : base(loop)
        {
            MusicType = musicType;
            BlendDuration = blendDuration;
        }

        public SoundPlayData_Music() : base(false)
        {
        }
    }
}