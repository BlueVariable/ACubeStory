using System;
using UnityEngine;

namespace BLUE.ACubeStory.Sound
{
    [Serializable]
    public class SoundSetting
    {
        [field: SerializeField] public ESound SoundType { get; private set; }
        [field: SerializeField] public AudioClip SoundClip { get; private set; }
    }

}