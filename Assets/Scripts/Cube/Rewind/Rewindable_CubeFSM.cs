using System;
using BLUE.ACubeStory.Cube.src;
using BLUE.ACubeStory.Cube.StateRewinder;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    [RequireComponent(typeof(IRewinderProvider))]
    public class Rewindable_CubeFSM : RewindableBase<RewindSnapshot_Cube>
    {
        [SerializeField] private FSM_Cube _cubeFsm = null;
        
        private IRewinderProvider _rewinderProvider;

        private IRewinderProvider _RewinderProvider
        {
            get
            {
                if (_rewinderProvider == null)
                {
                    _rewinderProvider = GetComponent<IRewinderProvider>();
                }

                return _rewinderProvider;
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
            CubeStateRewinderBase rewinder = _RewinderProvider.GetRewinderBase(state);

            if (rewinder == null)
            {
                UnityEngine.Debug.LogError($"{state} Rewinder could not be found.");
                
                return;
            }
            
            SaveSnapshot(new RewindSnapshot_Cube(rewinder));
        }
    }
}