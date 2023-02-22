using System;
using BLUE.ActionSystem;

namespace BLUE.ACubeStory.Cube
{
    public class Action_WaitForCubeStateUpdate : InterruptableActionBase
    {
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
            OnCompleted?.Invoke(this);
        }
    }
}

