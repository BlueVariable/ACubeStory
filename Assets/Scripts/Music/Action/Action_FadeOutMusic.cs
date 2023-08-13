using System;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Music
{
    public class Action_FadeOutMusic : InterruptableActionBase
    {
        [SerializeField] private float _fadeDuration = 5f;
        
        public override Action<InterruptableActionBase> OnCompleted { get; set; }
        
        public override void Execute()
        {
            SoundPlayer_Music.Instance.FadeOut(_fadeDuration, onFaded);

            void onFaded()
            {
                OnCompleted?.Invoke(this);
            }
        }
        
        public override void Interrupt()
        {
            if (SoundPlayer_Music.Instance == null)
            {
                return;
            }
            
            SoundPlayer_Music.Instance.Stop();
        }
    }
}