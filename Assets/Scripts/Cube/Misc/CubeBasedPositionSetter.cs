using System;
using System.Linq;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class CubeBasedPositionSetter : MonoBehaviour
    {
        [SerializeField] private Transform _movableObj = null;

        [SerializeField] private ECubeState[] _statesToUpdate = default;

        [SerializeField] private bool _usePreviousPos = true;

        private Vector3 _cachedPrevPos = new Vector3(Single.MaxValue, Single.MaxValue, Single.MaxValue);

        private bool _isCachedBefore = false;
        
        private void Awake()
        {
            Cube.Instance.CubeFSM.OnStateEntered += OnCubeStateEntered;
        }

        private void OnDestroy()
        {
            if (Cube.Instance == null)
            {
                return;
            }
            
            Cube.Instance.CubeFSM.OnStateEntered -= OnCubeStateEntered;
        }

        private void OnCubeStateEntered(ECubeState enteredState)
        {
            if (_statesToUpdate.Contains(enteredState))
            {
                if (!_usePreviousPos)
                {
                    _movableObj.position = Cube.Instance.GetPosition();
                }
                else
                {
                    if (_isCachedBefore)
                    {
                        _movableObj.position = _cachedPrevPos;
                    }
                    
                    _cachedPrevPos = Cube.Instance.GetPosition();
                    
                    _isCachedBefore = true;

                }
            }
        }
    }
}