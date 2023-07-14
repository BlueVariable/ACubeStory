using BLUE.ACubeStory.Checkpoint;
using BLUE.ACubeStory.Cube;
using BLUE.ACubeStory.Fail;
using UnityEngine;

namespace BLUE.ACubeStory.Moth
{
    public class CuriousMothFailObserver : MonoBehaviour, IFailObserver
    {
        [SerializeField] private Transform _mothRoot = null;
        
        public void FailObserved(IFailController failController)
        {
            if (failController is CubeFailController)
            {
                Checkpoint.Checkpoint lastCheckpoint = CheckpointManager.Instance.LastCheckpoint;

                if (lastCheckpoint == null)
                {
                    return;
                }

                CuriousMothRespawnPoint respawnPoint = lastCheckpoint.GetComponentInChildren<CuriousMothRespawnPoint>();

                if (respawnPoint == null)
                {
                    return;
                }

                _mothRoot.position = respawnPoint.transform.position;
            }
        }

        private void Awake()
        {
            FailManager.Instance.RegisterFailObserver(this);
        }

        private void OnDestroy()
        {
            if (FailManager.Instance == null)
            {
                return;
            }
            
            FailManager.Instance.UnregisterFailObserver(this);
        }
    }
}