using System;
using BLUE.SoundSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Sound
{
    public class SoundPlayerController_SerializedTypeBased : SoundPlayerControllerBase
    {
        [SerializeField] private ESound _soundType = ESound.None;

        private SoundPlayerBase _cachedPlayer = null;
        
        public override void AllocateSoundPlayer(Action<SoundPlayerBase> onSet)
        {
            if (_cachedPlayer)
            {
                onSet?.Invoke(_cachedPlayer);

                return;
            }
            
            AllocatePlayer(onPlayerSet);

            void onPlayerSet(SoundPlayerBase player)
            {
                _cachedPlayer = player;
                
                onSet?.Invoke(_cachedPlayer);
            }
        }

        public override void DeallocateSoundPlayer()
        {
            if (_cachedPlayer == null)
            {
                return;
            }
            
            SoundManager.Instance.DeallocateSoundPlayer(_cachedPlayer);

            _cachedPlayer = null;
        }

        private void AllocatePlayer(Action<SoundPlayerBase> onSet)
        {
            SoundManager.Instance.AllocateSoundPlayer(_soundType, onPlayerSet);

            void onPlayerSet(SoundPlayerBase player)
            {
                onSet?.Invoke(player);
            }
        }
    }
}