using BLUE.SoundSystem;

namespace BLUE.ACubeStory.Sound
{
    public class SoundGenerator_SoundMaker_Simple : SoundGenerator_SoundMakerBased<ESound, SoundMaker_TypeBased, ESoundMaker>
    {
        public override void GenerateSound(ESound soundType)
        {
            SoundManager.Instance.AllocateSoundPlayer(soundType, onSet);

            void onSet(SoundPlayerBase soundPlayer)
            {
                void onPlayed()
                {
                    SoundManager.Instance.DeallocateSoundPlayer(soundPlayer);
                }
                
                soundPlayer.Play(onPlayed);
            }
        }
    }
}