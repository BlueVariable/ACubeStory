using UnityEngine;
using BLUE.ActionSystem;

namespace BLUE.ACubeStory.Audio
{
    public class Action_AudioControllerSetCanPlayAudio : ActionBase
    {
        [SerializeField] private AudioControllerBase _audioController = null;

        [SerializeField] private bool _canPlayAudio = false;
        
        public override void Execute()
        {
            _audioController.SetCanPlayAudio(_canPlayAudio);
        }
    }
}