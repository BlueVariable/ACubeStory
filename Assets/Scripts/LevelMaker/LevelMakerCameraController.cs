using BLUE.InputSystem;
using UnityEngine;

namespace BLUE.ACubeStory.LevelMaker
{
    public class LevelMakerCameraController : MonoBehaviour
    {
        [SerializeField] private Transform _camera = null;
        
        [SerializeField] private float _cameraMovementSpeed = 2.5f;

        [SerializeField] private int _rotationMouseButton = 1;

        [SerializeField] private float _rotationSensivity = 0.1f;

        [SerializeField] private KeyCode _upwardsInput = KeyCode.Q;

        [SerializeField] private KeyCode _downwardsInput = KeyCode.E;
        
        private Vector3 _lastCursorPosition;
        
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
        }
        
        private void OnMouseButtonDown(InputData_Mouse inputData)
        {
            if (inputData.MouseButton != _rotationMouseButton)
            {
                return;
            }

            _lastCursorPosition = inputData.CursorPosition;

            InputManager.Instance.TryRegisterToInputListener<InputData_Mouse>(
                EInputEvent.MouseButtonHold,
                OnMouseButtonHold);
            
            InputManager.Instance.TryRegisterToInputListener<InputData_Axis>(
                EInputEvent.HorizontalAxis,
                OnHorizontalInputPerformed);

            InputManager.Instance.TryRegisterToInputListener<InputData_Axis>(
                EInputEvent.VerticalAxis,
                OnVerticalInputPerformed);
            
            InputManager.Instance.TryRegisterToInputListener<InputData_Key>(
                EInputEvent.KeyHold,
                OnKeyHold);
        }

        private void OnMouseButtonUp(InputData_Mouse inputData)
        {
            if (inputData.MouseButton != _rotationMouseButton)
            {
                return;
            }
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Mouse>(
                EInputEvent.MouseButtonHold,
                OnMouseButtonHold);
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Axis>(
                EInputEvent.HorizontalAxis,
                OnHorizontalInputPerformed);

            InputManager.Instance.TryUnregisterFromInputListener<InputData_Axis>(
                EInputEvent.VerticalAxis,
                OnVerticalInputPerformed);
            
            InputManager.Instance.TryUnregisterFromInputListener<InputData_Key>(
                EInputEvent.KeyHold,
                OnKeyHold);
        }

        private void OnMouseButtonHold(InputData_Mouse inputData)
        {
            if (inputData.MouseButton != _rotationMouseButton)
            {
                return;
            }

            Vector3 delta = inputData.CursorPosition - _lastCursorPosition;
            
            UnityEngine.Debug.Log(delta);

            delta = new Vector3(-delta.y, delta.x, 0);

            Vector3 deltaRotation = delta * (_rotationSensivity * Time.deltaTime);
            
            _camera.localRotation = 
                Quaternion.Euler(_camera.localRotation.eulerAngles + deltaRotation);

            _lastCursorPosition = inputData.CursorPosition;
        }

        private void OnHorizontalInputPerformed(InputData_Axis inputData)
        {
            _camera.position +=
                _camera.right * (inputData.Amount * Time.deltaTime * _cameraMovementSpeed);
        }

        private void OnVerticalInputPerformed(InputData_Axis inputData)
        {
            _camera.position +=
                _camera.forward * (inputData.Amount * Time.deltaTime * _cameraMovementSpeed);
        }

        private void OnKeyHold(InputData_Key inputData)
        {
            if (inputData.KeyCode != _upwardsInput && 
                inputData.KeyCode != _downwardsInput)
            {
                return;
            }
            
            int sign = inputData.KeyCode == _upwardsInput ? 1 : -1;

            _camera.position += _camera.up * (sign * _cameraMovementSpeed * Time.deltaTime);
        }
    }
}