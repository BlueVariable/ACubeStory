using UnityEngine;
using BLUE.InputSystem;

namespace BLUE.ACubeStory.LevelMaker
{
    [RequireComponent(typeof(ITileProvider))]
    public class TilePlacer : MonoBehaviour
    {
        [SerializeField] private Transform _placementHighlight = null;
        
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
        
        private void Awake()
        {
            _placementHighlight.transform.gameObject.SetActive(false);
            
            RegisterToInputManager();
        }

        private void OnDestroy()
        {
            UnregisterFromInputManager();
        }

        private void RegisterToInputManager()
        {
            InputManager.Instance.TryRegisterToInputListener<InputData_Mouse>(EInputEvent.MouseButtonUp, OnMouseButtonUp);
            InputManager.Instance.TryRegisterToInputListener<InputData_Mouse>(EInputEvent.MouseButtonHold, OnMouseHold);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Mouse>(EInputEvent.MouseButtonUp, OnMouseButtonUp);
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Mouse>(EInputEvent.MouseButtonHold, OnMouseHold);
        }

        private void OnMouseButtonUp(InputData_Mouse inputData)
        {
            if (inputData.MouseButton != _targetMouseButton)
            {
                return;
            }
            
            TryToPlaceTile(inputData.CursorPosition);
            
            _placementHighlight.gameObject.SetActive(false);
        }

        private void OnMouseHold(InputData_Mouse inputData)
        {
            if (inputData.MouseButton != _targetMouseButton)
            {
                return;
            }
            
            TryHighlightTilePlacement(inputData.CursorPosition);
        }

        private void TryToPlaceTile(Vector3 screenPosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _tilePlacementDistance))
            {
                return;
            }

            GameObject tileObject = Instantiate(_TileProvider.GetTile().gameObject);

            tileObject.transform.position = ray.origin + ray.direction * _tilePlacementDistance;
        }

        private void TryHighlightTilePlacement(Vector3 screenPosition)
        {
            _placementHighlight.gameObject.SetActive(true);
            
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _tilePlacementDistance)) 
            {
                if (hitInfo.collider.TryGetComponent(out Tile neighborCandidate))
                {
                    Vector3 placementOrigin = neighborCandidate.transform.position;
                    
                    _placementHighlight.transform.position = placementOrigin + hitInfo.normal;        
                }
                
                return;
            }

            _placementHighlight.transform.position = ray.origin + ray.direction * _tilePlacementDistance;
        }
    }
}