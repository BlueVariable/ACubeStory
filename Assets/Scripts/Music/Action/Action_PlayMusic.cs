using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Music
{
    public class Action_PlayMusic : ActionBase
    {
        [SerializeField] private SoundPlayData_Music _musicPlayData = null;
        
        public override void Execute()
        {
            SoundPlayer_Music.Instance.Play(_musicPlayData);
        }
    }
}