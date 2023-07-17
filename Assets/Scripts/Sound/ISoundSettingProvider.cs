namespace BLUE.ACubeStory.Sound
{
    public interface ISoundSettingProvider
    {
        public bool TryGetSoundSettings(
            ESound sound,
            out SoundSetting soundSetting);
    }
}