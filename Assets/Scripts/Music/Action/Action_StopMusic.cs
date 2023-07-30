using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Music
{
    public class Action_StopMusic : ActionBase
    {
        public override void Execute()
        {
            SoundPlayer_Music.Instance.Stop();
        }
    }
}