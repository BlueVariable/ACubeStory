using System.Collections.Generic;
using BLUE.RewindSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Rewindable_Cube_Platform : RewindableBase<RewindController_Cube, RewindSnapshot_CubeBase>
    {
        [SerializeField] private FSM_Cube _cubeFSM = null;

        private Dictionary<ECubeState, CubeRewindSnapshotFactoryBase> _snapshotFactories;
        private Dictionary<ECubeState, CubeRewindSnapshotFactoryBase> _SnapshotFactories
        {
            get
            {
                if (!_snapshotFactoriesInited)
                {
                    InitSnapshotFactories();
                }

                return _snapshotFactories;
            }
        }

        private bool _snapshotFactoriesInited;
        
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
            _cubeFSM.OnStateEntered += OnCubeEnteredState;
        }

        private void UnregisterFromCubeFSM()
        {
            _cubeFSM.OnStateEntered -= OnCubeEnteredState;
        }

        private void OnCubeEnteredState(ECubeState state)
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
            
            void onSnapshotCreated(RewindSnapshot_CubeBase snapshot)
            {
                if (snapshot != null)
                {
                    SaveSnapshot(snapshot);
                }
            }
        }

        private void InitSnapshotFactories()
        {
            _snapshotFactories = new Dictionary<ECubeState, CubeRewindSnapshotFactoryBase>();
                
            foreach (CubeRewindSnapshotFactoryBase factory in GetComponentsInChildren<CubeRewindSnapshotFactoryBase>())
            {
                _snapshotFactories.Add(factory.GetCubeState(), factory);
            }

            _snapshotFactoriesInited = true;
        }
    }
}