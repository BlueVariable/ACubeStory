using System;
using BLUE.RewindSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class RewindController_CubeFSM : RewindControllerBase
    {
        [SerializeField] private FSM_Cube _cubeFsm = null;
        
        private void Awake()
        {
            RegisterToCubeFSM();
        }

        private void OnDestroy()
        {
            UnregisterFromCubeFSM();
        }

        #region Event Registiration

        private void RegisterToCubeFSM()
        {
            _cubeFsm.OnStateEntered += OnCubeFSMEnterState;
        }

        private void UnregisterFromCubeFSM()
        {
            _cubeFsm.OnStateEntered -= OnCubeFSMEnterState;
        }

        #endregion
        
        private void OnCubeFSMEnterState(ECubeState eCubeState)
        {
            // save snapshot
        }
    }
}