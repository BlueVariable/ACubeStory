using BLUE.ACubeStory.Cube;
using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.Platform
{
    public class PlatformObjectUpdater_CubeMoved : PlatformObjectContainerUpdater
    {
        [SerializeField] private List<ECubeState> _updateStates = new List<ECubeState>();

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
            Cube.Cube.Instance.CubeFSM.OnStateExited += OnCubeStateExited;
        }

        private void UnregisterFromCubeEvents()
        {
            if (Cube.Cube.Instance == null)
            {
                return;
            }
            
            Cube.Cube.Instance.CubeFSM.OnStateExited -= OnCubeStateExited;
        }

        private void OnCubeStateExited(ECubeState cubeState)
        {
            if (!_updateStates.Contains(cubeState))
            {
                return;
            }

            UpdatePlatformObjectContainer();
        }
    }
}