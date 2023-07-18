using BLUE.SoundSystem;

namespace BLUE.ACubeStory.Sound
{
    public class SoundGenerator_SoundMaker_Simple : SoundGenerator_SoundMakerBased<ESound, SoundMaker_Simple, ESoundMaker>
    {
        public override void GenerateSound(ESound soundType)
        {
            SoundManager.Instance.GetSoundPlayer(soundType, onSet);

            void onSet(SoundPlayerBase soundPlayer)
            {
                soundPlayer.Play();
            }
        }
    }
}