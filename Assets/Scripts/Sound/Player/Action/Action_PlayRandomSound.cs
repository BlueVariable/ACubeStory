using System;
using BLUE.ActionSystem;
using BLUE.Extensions;
using BLUE.SoundSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Sound
{
    public class Action_PlayRandomSound : InterruptableActionBase
    {
        [SerializeField] private SoundPlayerControllerBase[] _soundPlayerControllers = null;

        #region Events
        
        public override Action<InterruptableActionBase> OnCompleted { get; set; }
        
        #endregion

        private SoundPlayerControllerBase _activeSoundPlayerController;
        
        public override void Execute()
        {
            Interrupt();
            
            _activeSoundPlayerController = _soundPlayerControllers.GetRandomElement();
            
            _activeSoundPlayerController.AllocateSoundPlayer(onPlayerSet);

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
            if (_activeSoundPlayerController == null)
            {
                return;
            }
            
            _activeSoundPlayerController.DeallocateSoundPlayer();
        }
    }
}