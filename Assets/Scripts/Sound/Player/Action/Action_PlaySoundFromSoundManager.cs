using System;
using BLUE.ActionSystem;
using BLUE.Extensions;
using BLUE.SoundSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Sound
{
    public class Action_PlaySoundFromSoundManager : InterruptableActionBase
    {
        [SerializeField] private ESound[] _soundTypes = default;

        #region Events
        
        public override Action<InterruptableActionBase> OnCompleted { get; set; }
        
        #endregion

        public override void Execute()
        {
            ESound randSound = _soundTypes.GetRandomElement();

            SoundManager.Instance.PlaySound(randSound);
        }
        
        public override void Interrupt()
        {

        }
    }
}