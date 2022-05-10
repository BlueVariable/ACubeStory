using UnityEngine;
using BLUE.InputSystem;
using UnityEngine.EventSystems;

namespace BLUE.ACubeStory.LevelEditor
{
    [RequireComponent(typeof(ITileProvider))]
    public class TilePlacer : MonoBehaviour
    {
        [SerializeField] private TileHighlighter _highlighter = null;
        
        [SerializeField] private float _tilePlacementDistance = 25f;

        [SerializeField] private int _targetMouseButton = 0;

        [SerializeField] private Color _correctPlacementHighlightColor = default;
        
        [SerializeField] private Color _incorrectPlacementHighlightColor = default;
        
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

        /*private TilePlacementConditionBase[] _placementConditions;

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
        }*/

        public bool CheckAllPlacementConditionsAreSatisfied(Vector3 pos)
        {
            bool satisfied = true;
            
            /*foreach (TilePlacementConditionBase placementCondition in _PlacementConditions)
            {
                satisfied = satisfied && placementCondition.IsSatisfied(pos);
            }*/

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
            InputManager.Instance.TryRegisterToInputListener<InputData_Mouse>(
                EInputEvent.MouseButtonDown,
                OnMouseButtonDown);
            
            InputManager.Instance.TryRegisterToInputListener<InputData_Mouse>(
                EInputEvent.MouseButtonUp,
                OnMouseButtonUp);
            
            InputManager.Instance.TryRegisterToInputListener<InputData_Mouse>(
                EInputEvent.MouseButtonHold,
                OnMouseHold);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Mouse>(
                EInputEvent.MouseButtonDown,
                OnMouseButtonDown);
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Mouse>(
                EInputEvent.MouseButtonUp, 
                OnMouseButtonUp);
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Mouse>(
                EInputEvent.MouseButtonHold,
                OnMouseHold);
        }

        private void OnMouseButtonDown(InputData_Mouse inputData)
        {
            if (inputData.MouseButton != _targetMouseButton)
            {
                return;
            }
            
            if (_highlighter != null)
            {
                _highlighter.SetHighlightEnabled(true);
                
                if (CalculatePlacementPosition(inputData.CursorPosition, out Vector3 highlightPos))
                {
                    HighlightPosition(highlightPos);
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

            if (_highlighter != null)
            {
                _highlighter.SetHighlightEnabled(false);
            }
        }

        private void OnMouseHold(InputData_Mouse inputData)
        {
            if (inputData.MouseButton != _targetMouseButton)
            {
                return;
            }

            if (_highlighter != null &&
                CalculatePlacementPosition(inputData.CursorPosition, out Vector3 highlightPos))
            {
                _highlighter.SetHighlightEnabled(true);

                HighlightPosition(highlightPos);
                
                return;
            }
            
            _highlighter.SetHighlightEnabled(false);
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

            TileFactory.Instance.TryCreateTile(_TileProvider.GetTileID(), position, out Tile createdTile);
        }

        private bool CalculatePlacementPosition(Vector3 screenPosition, out Vector3 placementPos)
        {
            placementPos = Vector3.zero;

            if (EventSystem.current.IsPointerOverGameObject())
            {
                return false;
            }
            
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity)) 
            {
                if (hitInfo.collider.TryGetComponent(out Tile neighborCandidate))
                {
                    placementPos = neighborCandidate.transform.position + hitInfo.normal;
                    
                    Debug.LogError(placementPos);

                    return true;
                }
            }

            placementPos = ray.origin + ray.direction.normalized * _tilePlacementDistance;

            return true;
        }

        private void HighlightPosition(Vector3 highlightPosition)
        {
            bool isCorrectPlacement = CheckAllPlacementConditionsAreSatisfied(highlightPosition);

            Color highlightColor = isCorrectPlacement
                ? _correctPlacementHighlightColor
                : _incorrectPlacementHighlightColor;
                    
            _highlighter.HighlightPosition(new TileHighlightData(highlightPosition, highlightColor));
        }
    }
}