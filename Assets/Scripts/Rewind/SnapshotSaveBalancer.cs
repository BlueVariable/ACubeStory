using System.Collections.Generic;

namespace BLUE.ACubeStory.Rewind
{
    public class SnapshotSaveBalancer
    {
        public int SaveSnapshotFactor { get; private set; } = 1;

        private int _snapshotFactorMultiplier = 2;

        public SnapshotSaveBalancer(int saveSnapshotFactor)
        {
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

            SaveSnapshotFactor *= _snapshotFactorMultiplier;

            for (int i = 0; i < savedSnapshots.Count; i += SaveSnapshotFactor)
            {
                balancedList.Add(savedSnapshots[i]);
            }
            
            savedSnapshots.Clear();

            return balancedList;
        }
    }
}