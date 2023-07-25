using System;
using BLUE.ActionSystem;
using BLUE.SoundSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Sound
{
    public class Action_PlaySound : InterruptableActionBase
    {
        [SerializeField] private SoundPlayerControllerBase _soundPlayerController = null;

        #region Events
        public override Action<InterruptableActionBase> OnCompleted { get; set; }
        #endregion
        
        public override void Execute()
        {
            _soundPlayerController.AllocateSoundPlayer(onPlayerSet);

            void onPlayerSet(SoundPlayerBase soundPlayer)
            {
                soundPlayer.Play(onCompleted);
            }

            void onCompleted()
            {
                Interrupt();

                OnCompleted?.Invoke(this);
            }
        }
        
        public override void Interrupt()
        {
            _soundPlayerController.DeallocateSoundPlayer();
        }
    }
}