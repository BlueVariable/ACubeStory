using System;
using System.Collections.Generic;
using BLUE.ACubeStory.Cube;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class PatrolTrigger_CubeMoved : PatrolTriggerBase
    {
        [SerializeField] private List<ECubeState> _triggerStates = new List<ECubeState>();

        public override Action OnPatrolTriggered { get; set; }

        private void Awake()
        {
            RegisterToCubeEvents();
        }

        private void OnDestroy()
        {
            UnregisterFromCubeEvents();
        }

        private void RegisterToCubeEvents()
        {
            Cube.Cube.Instance.CubeFSM.OnStateEntered += OnCubeEnteredState;
        }

        private void UnregisterFromCubeEvents()
        {
            Cube.Cube.Instance.CubeFSM.OnStateEntered -= OnCubeEnteredState;   
        }

        private void OnCubeEnteredState(ECubeState cubeState)
        {
            if (!_triggerStates.Contains(cubeState))
            {
                return;
            }
            
            OnPatrolTriggered?.Invoke();
        }
    }
}