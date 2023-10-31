using System.Linq;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeBasedPositionSetter : MonoBehaviour
    {
        [SerializeField] private Transform _movableObj = null;

        [SerializeField] private ECubeState[] _statesToUpdateOnEnter = default;
        [SerializeField] private ECubeState[] _statesToUpdateOnExit = default;

        private bool _isCachedBefore = false;
        
        private void Awake()
        {
            Cube.Instance.CubeFSM.OnStateEntered += OnCubeStateEntered;
            Cube.Instance.CubeFSM.OnStateExited += OnCubeStateExited;
        }

        private void OnDestroy()
        {
            if (Cube.Instance == null)
            {
                return;
            }
            
            Cube.Instance.CubeFSM.OnStateEntered -= OnCubeStateEntered;
            Cube.Instance.CubeFSM.OnStateExited -= OnCubeStateExited;
        }



        private void OnCubeStateEntered(ECubeState enteredState)
        {
            if (_statesToUpdateOnEnter.Contains(enteredState))
            {
                _movableObj.position = Cube.Instance.GetPosition();
            }
        }
        
        private void OnCubeStateExited(ECubeState exitedState)
        {
            if (_statesToUpdateOnExit.Contains(exitedState))
            {
                _movableObj.position = Cube.Instance.GetPosition();
            }
        }
    }
}