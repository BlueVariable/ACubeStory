using UnityEngine;
using BLUE.ACubeStory.Cube.src;
using System.Collections.Generic;

namespace BLUE.ACubeStory.Cube
{
    public class Rewindable_Cube_Platform : RewindableBase<RewindController_Cube, RewindSnapshot_Cube>
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

            _SnapshotFactories[state].CreateSnapshot(onSnapshotCreated);
            
            void onSnapshotCreated(RewindSnapshot_Cube snapshot)
            {
                SaveSnapshot(snapshot);
            }
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