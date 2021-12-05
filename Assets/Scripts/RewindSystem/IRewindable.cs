namespace BLUE.ACubeStory.RewindSystem
{
    public interface IRewindable
    {
        void ActivateRewindable(RewindController rewindController);
        
        void DeactivateRewindable(RewindController rewindController);
        
        void SaveSnapshot();

        void ResetSavedSnapshots();

        void StartRewind();
        
        void StopRewind();
    }
}