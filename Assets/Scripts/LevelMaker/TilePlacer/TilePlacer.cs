using UnityEngine;
using BLUE.InputSystem;

namespace BLUE.ACubeStory.LevelMaker
{
    [RequireComponent(typeof(ITileProvider))]
    public class TilePlacer : MonoBehaviour
    {
        [SerializeField] private float _tilePlacementDistance = 25f;
        
        [SerializeField] private int _targetMouseButton = 0;
        
        private ITileProvider _tileProvider;

        private ITileProvider _TileProvider
        {
            get
            {
                if (_tileProvider == null)
                {
                    _tileProvider = GetComponent<ITileProvider>();
                }

                return _tileProvider;
            }
        }

        private TilePlacementHighlighter _highlighter;

        private TilePlacementHighlighter _Highlighter
        {
            get
            {
                if (_highlighter == null)
                {
                    _highlighter = GetComponent<TilePlacementHighlighter>();
                }

                return _highlighter;
            }
        }

        private TilePlacementConditionBase[] _placementConditions;

        private TilePlacementConditionBase[] _PlacementConditions
        {
            get
            {
                if (_placementConditions == null)
                {
                    _placementConditions = GetComponentsInChildren<TilePlacementConditionBase>();
                }

                return _placementConditions;
            }
        }

        public bool CheckAllPlacementConditionsAreSatisfied(Vector3 pos)
        {
            bool satisfied = true;
            
            foreach (TilePlacementConditionBase placementCondition in _PlacementConditions)
            {
                satisfied = satisfied && placementCondition.IsSatisfied(pos);
            }

            return satisfied;
        }
        
        private void Awake()
        {
            RegisterToInputManager();
        }

        private void OnDestroy()
        {
            UnregisterFromInputManager();
        }

        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Mouse>(EInputEvent.MouseButtonDown, OnMouseButtonDown);
            InputManager.Instance.TryRegisterToInputListener<InputData_Mouse>(EInputEvent.MouseButtonUp, OnMouseButtonUp);
            InputManager.Instance.TryRegisterToInputListener<InputData_Mouse>(EInputEvent.MouseButtonHold, OnMouseHold);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Mouse>(EInputEvent.MouseButtonDown, OnMouseButtonDown);
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Mouse>(EInputEvent.MouseButtonUp, OnMouseButtonUp);
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Mouse>(EInputEvent.MouseButtonHold, OnMouseHold);
        }

        private void OnMouseButtonDown(InputData_Mouse inputData)
        {
            if (inputData.MouseButton != _targetMouseButton)
            {
                return;
            }
            
            if (_Highlighter != null)
            {
                _Highlighter.SetHighlightEnabled(true);
                
                if (CalculatePlacementPosition(inputData.CursorPosition, out Vector3 highlightPos))
                {
                    _Highlighter.HighlightPlacement(highlightPos);
                }
            }
        }

        private void OnMouseButtonUp(InputData_Mouse inputData)
        {
            if (inputData.MouseButton != _targetMouseButton)
            {
                return;
            }
            
            TryToPlaceTile(inputData.CursorPosition);

            if (_Highlighter != null)
            {
                _Highlighter.SetHighlightEnabled(false);
            }
        }

        private void OnMouseHold(InputData_Mouse inputData)
        {
            if (inputData.MouseButton != _targetMouseButton)
            {
                return;
            }

            if (_Highlighter != null && 
                CalculatePlacementPosition(inputData.CursorPosition, out Vector3 highlightPos))
            {
                _Highlighter.HighlightPlacement(highlightPos);
            }
        }

        private void TryToPlaceTile(Vector3 screenPosition)
        {
            if (!CalculatePlacementPosition(screenPosition, out Vector3 position))
            {
                return;
            }

            if (!CheckAllPlacementConditionsAreSatisfied(position))
            {
                return;
            }

            GameObject tileObject = Instantiate(_TileProvider.GetTile().gameObject);

            tileObject.transform.position = position;
        }

        private bool CalculatePlacementPosition(Vector3 screenPosition, out Vector3 placementPos)
        {
            placementPos = Vector3.zero;
            
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _tilePlacementDistance)) 
            {
                if (hitInfo.collider.TryGetComponent(out Tile neighborCandidate))
                {
                    placementPos = neighborCandidate.transform.position + hitInfo.normal;

                    return true;
                }
            }

            placementPos = ray.origin + ray.direction.normalized * _tilePlacementDistance;

            return true;
        }
    }
}