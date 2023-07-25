using BLUE.ActionSystem;
using BLUE.SoundSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Sound
{
    public class Action_StopSound : ActionBase
    {
        [SerializeField] private SoundPlayerControllerBase _soundPlayerController = null;

        public override void Execute()
        {
            _soundPlayerController.DeallocateSoundPlayer();
        }
    }
}