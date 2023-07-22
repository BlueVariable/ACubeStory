using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class MergePuzzle : MonoBehaviour
    {
        #region Singleton

        private static MergePuzzle _instance;
        public static MergePuzzle Instance
        {
            get
            {
                // TODO: Merge
                return null;
                
                if (_instance == null)
                {
                    _instance = FindObjectOfType<MergePuzzle>();
                }

                return _instance;
            }
        }

        #endregion

        private MergeableProvider _mergeableProvider = null;
        public MergeableProvider MergeableProvider
        {
            get
            {
                if (_mergeableProvider == null)
                {
                    _mergeableProvider = GetComponentInChildren<MergeableProvider>();
                }

                return _mergeableProvider;
            }
        }
        
        private MergeableSlotManager _mergeableSlotManager = null;
        public MergeableSlotManager MergeableSlotManager
        {
            get
            {
                if (_mergeableSlotManager == null)
                {
                    _mergeableSlotManager = GetComponentInChildren<MergeableSlotManager>();
                }

                return _mergeableSlotManager;
            }
        }
    }
}