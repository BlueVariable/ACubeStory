using System;

namespace BLUE.ACubeStory.Rewind
{
    public interface IRewindable
    {
        #region Events

        public Action<IRewindable> OnRewound { get; set; }

        #endregion
        
        public void ActivateRewindable(RewindController rewindController);
        
        public void DeactivateRewindable(RewindController rewindController);
        
        public bool TrySaveSnapshot(SnapshotSaveBalancer saveBalancer);

        public void ResetSavedSnapshots();

        public void StartRewind();
        
        public void StopRewind();

        public bool CanSaveNextSnapshot();
    }
}
