using BLUE.ACubeStory.Sound;
using BLUE.Extensions;
using BLUE.SoundSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Common

{
    public class RollSFXController : MonoBehaviour
    {
        [SerializeField] private RollBehaviourBase _rollBehaviour = null;

        [SerializeField] private ESound[] _soundTypes = default;
        
        private void Awake()
        {
            _rollBehaviour.OnRollStarted += OnRollStarted;
        }

        private void OnDestroy()
        {
            _rollBehaviour.OnRollStarted -= OnRollStarted;
        }

        private void OnRollStarted()
        {
            ESound randSound = _soundTypes.GetRandomElement();

            void onSet(SoundPlayerBase soundPlayer)
            {
                void onPlayed()
                {
                    SoundManager.Instance.DeallocateSoundPlayer(soundPlayer);
                }
                
                soundPlayer.Play(onPlayed);
            }
            
            SoundManager.Instance.AllocateSoundPlayer(randSound, onSet);
        }
    }
}