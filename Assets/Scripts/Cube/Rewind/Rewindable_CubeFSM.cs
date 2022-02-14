using System.Collections.Generic;
using UnityEngine;
using BLUE.ACubeStory.Cube.src;

namespace BLUE.ACubeStory.Cube
{
    public class Rewindable_CubeFSM : RewindableBase<RewindSnapshot_Cube>
    {
        [SerializeField] private FSM_Cube _cubeFsm = null;

        private Dictionary<ECubeState, CubeSnapshotFactory> _snapshotFactories;

        private Dictionary<ECubeState, CubeSnapshotFactory> _SnapshotFactories
        {
            get
            {
                InitSnapshotFactories();
                
                return _snapshotFactories;
            }
        }
        
        private void Awake()
        {
            RegisterToCubeFSM();
        }

        private void OnDestroy()
        {
            UnregisterFromCubeFSM();
        }

        private void RegisterToCubeFSM()
        {
            _cubeFsm.OnStateEntered += OnCubeFSMEnterState;
        }

        private void UnregisterFromCubeFSM()
        {
            _cubeFsm.OnStateEntered -= OnCubeFSMEnterState;
        }

        private void OnCubeFSMEnterState(ECubeState state)
        {
            CreateAndSaveSnapshot(state);
        }

        private void CreateAndSaveSnapshot(ECubeState state)
        {
            if (!_SnapshotFactories.ContainsKey(state))
            {
                return;
            }

            RewindSnapshot_Cube snapshot = _SnapshotFactories[state].CreateSnapshot();
            
            SaveSnapshot(snapshot);
        }

        private void InitSnapshotFactories()
        {
            _snapshotFactories = new Dictionary<ECubeState, CubeSnapshotFactory>();
                
            foreach (CubeSnapshotFactory factory in GetComponentsInChildren<CubeSnapshotFactory>())
            {
                _snapshotFactories.Add(factory.GetCubeState(), factory);
            }
        }
    }
}