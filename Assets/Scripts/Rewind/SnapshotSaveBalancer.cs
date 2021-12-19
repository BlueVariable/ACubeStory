using System.Collections.Generic;

namespace BLUE.ACubeStory.Rewind
{
    public class SnapshotSaveBalancer
    {
        private float _snapshotFactorMultiplier;
        
        public int SaveSnapshotFactor { get; private set; }

        public SnapshotSaveBalancer(float snapshotFactorMultiplier, int saveSnapshotFactor)
        {
            _snapshotFactorMultiplier = snapshotFactorMultiplier;
            SaveSnapshotFactor = saveSnapshotFactor;
        }

        public List<T> BalanceSnapshots<T>(List<T> savedSnapshots, int maxSnapshotCount)
        where T : RewindSnapshotBase
        {
            if (savedSnapshots.Count < maxSnapshotCount)
            {
                return savedSnapshots;
            }

            List<T> balancedList = new List<T>();

            SaveSnapshotFactor = (int)(SaveSnapshotFactor * _snapshotFactorMultiplier);

            for (int i = 0; i < savedSnapshots.Count; i += SaveSnapshotFactor)
            {
                balancedList.Add(savedSnapshots[i]);
            }
            
            savedSnapshots.Clear();

            return balancedList;
        }
    }
}