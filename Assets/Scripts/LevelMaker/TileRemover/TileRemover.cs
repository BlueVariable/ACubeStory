using BLUE.InputSystem;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TileRemover : MonoBehaviour
    {
        [SerializeField] private TileHighlighter _highlighter = null;
        
        [SerializeField] private int _deletionMouseButton = 2;
        
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
                EInputEvent.MouseButtonHold,
                OnMouseButtonHold);
            
            InputManager.Instance.TryRegisterToInputListener<InputData_Mouse>(
                EInputEvent.MouseButtonUp,
                OnMouseButtonUp);
        }

        private void UnregisterFromInputManager()
        {
            if (InputManager.Instance == null)
            {
                return;
            }

            InputManager.Instance.TryUnregisterFromInputListener<InputData_Mouse>(
                EInputEvent.MouseButtonHold,
                OnMouseButtonHold);
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Mouse>(
                EInputEvent.MouseButtonUp,
                OnMouseButtonUp);
        }
        

        private void OnMouseButtonHold(InputData_Mouse inputData)
        {
            if (inputData.MouseButton != _deletionMouseButton)
            {
                return;
            }

            if (TryGetTileToRemove(inputData.CursorPosition, out Tile deletionCandidate))
            {
                _highlighter.SetHighlightEnabled(true);
                
                _highlighter.HighlightPosition(
                    new TileHighlightData(deletionCandidate.transform.position, Color.red));
                
                return;
            }
            
            _highlighter.SetHighlightEnabled(false);
        }

        private void OnMouseButtonUp(InputData_Mouse inputData)
        {
            if (inputData.MouseButton != _deletionMouseButton)
            {
                return;
            }
            
            if (TryGetTileToRemove(inputData.CursorPosition, out Tile deletionCandidate))
            {
                Destroy(deletionCandidate.gameObject);
            }
            
            _highlighter.SetHighlightEnabled(false);
        }

        private bool TryGetTileToRemove(Vector3 screenPosition, out Tile tile)
        {
            tile = null;
            
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity)) 
            {
                if (hitInfo.collider.TryGetComponent(out tile))
                {
                    return true;
                }
            }

            return false;
        }
    }
}