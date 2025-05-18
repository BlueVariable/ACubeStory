using System;
using System.Linq;
using BLUE.ActionSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class Action_WaitForCubeStateUpdate : InterruptableActionBase
    {
        [SerializeField] private ECubeState[] _validStates = new ECubeState[]{};
        
        #region Events

        public override Action<InterruptableActionBase> OnCompleted { get; set; }

        #endregion

        public override void Execute()
        {
            UnregisterFromCubeFSM();
            
            RegisterToCubeFSM();
        }

        
        public override void Interrupt()
        {
            UnregisterFromCubeFSM();
        }

        private void OnDestroy()
        {
            UnregisterFromCubeFSM();
        }

        private void RegisterToCubeFSM()
        {
            Cube.Instance.CubeFSM.OnStateEntered += OnCubeStateEntered;
        }
        
        private void UnregisterFromCubeFSM()
        {
            if (Cube.Instance == null)
            {
                return;
            }
            
            Cube.Instance.CubeFSM.OnStateEntered -= OnCubeStateEntered;
        }

        private void OnCubeStateEntered(ECubeState cubeState)
        {
            if (_validStates.Length > 0)
            {
                if (!_validStates.Contains(cubeState))
                {
                    return;
                }
            }
            
            OnCompleted?.Invoke(this);
        }
    }
}

