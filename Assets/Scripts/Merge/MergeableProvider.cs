using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class MergeableProvider : MonoBehaviour
    {
        private Mergeable[] _allMergeables;
        public Mergeable[] AllMergeables
        {
            get
            {
                if (_allMergeables == null)
                {
                    _allMergeables = FindObjectsOfType<Mergeable>();
                }

                return _allMergeables;
            }
        }
    }
}