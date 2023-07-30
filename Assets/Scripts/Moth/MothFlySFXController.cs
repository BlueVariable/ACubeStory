using BLUE.Common;
using BLUE.SoundSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Moth
{
    public class MothFlySFXController : ActivatableBehaviourBase
    {
        [SerializeField] private bool _activeOnAwake = true;
        
        [SerializeField] private AnimationController_Moth _mothAnimController = null;

        [SerializeField] private SoundPlayerControllerBase _soundPlayerController = null;
        
        public override void Activate()
        {
            RegisterToMothAnimController();

            if (_mothAnimController.IsCurPlayingAnim(EMothAnimation.Fly))
            {
                PlayFlySFX();
            }
        }

        public override void Deactivate()
        {
            UnregisterFromMothAnimController();

            StopFlySFX();
        }
        
        private void Awake()
        {
            if (_activeOnAwake)
            {
                Activate();
            }
        }

        private void OnDestroy()
        {
            UnregisterFromMothAnimController();

            StopFlySFX();
        }

        private void RegisterToMothAnimController()
        {
            _mothAnimController.OnAnimationStateUpdated += OnAnimationStateUpdated;
        }
        
        private void UnregisterFromMothAnimController()
        {
            _mothAnimController.OnAnimationStateUpdated -= OnAnimationStateUpdated;
        }

        private void OnAnimationStateUpdated(EMothAnimation mothAnim, int layerIndex)
        {
            if (mothAnim == EMothAnimation.Fly)
            {
                PlayFlySFX();
            }
            else
            {
                StopFlySFX();
            }
        }

        private void PlayFlySFX()
        {
            StopFlySFX();
            
            _soundPlayerController.AllocateSoundPlayer(onPlayerSet);

            void onPlayerSet(SoundPlayerBase soundPlayer)
            {
                SoundPlayer_Simple simplePlayer = soundPlayer as SoundPlayer_Simple;
                
                simplePlayer.Play(new SoundPlayData_Simple(true), onCompleted);
            }

            void onCompleted()
            {
                StopFlySFX();
            }
        }

        private void StopFlySFX()
        {
            _soundPlayerController.DeallocateSoundPlayer();
        }
    }
}