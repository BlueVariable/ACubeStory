using UnityEngine;

namespace BLUE.ACubeStory.Cube.src
{
    public abstract class RewindableBase<T1, T2> : MonoBehaviour
    where T1 : RewindControllerBase<T2>
    where T2 : RewindSnapshotBase
    {

        private T1 _rewindController;

        private T1 _RewindController
        {
            get
            {
                if (_rewindController == null)
                {
                    _rewindController = GetComponentInParent<T1>();
                }

                return _rewindController;
            }
        }
        
        protected void SaveSnapshot(T2 snapshot)
        {
            _RewindController.SaveSnapshot(snapshot);
        }
    }
}