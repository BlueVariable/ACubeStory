using BLUE.SoundSystem;

namespace BLUE.ACubeStory.Sound
{
    public class SoundGenerator_SoundRequestor_Simple : SoundGenerator_SoundRequestorBased<ESound, SoundRequestor_TypeBased, ESoundRequestor>
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