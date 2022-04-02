using UnityEngine;

namespace BLUE.ACubeStory.UI
{
    public class Cursor : MonoBehaviour
    {
        [SerializeField] private RectTransform _cursorTransform = null;

        [SerializeField] private Canvas _parentCanvas = null;
        
        private void Update()
        {
            UpdateCursorPosition(Input.mousePosition);
        }

        private void UpdateCursorPosition(Vector3 screenPos)
        {
            float canvasScaleFactor = _parentCanvas.scaleFactor;

            Vector2 canvasPos = new Vector2(screenPos.x / canvasScaleFactor, screenPos.y / canvasScaleFactor);

            _cursorTransform.anchoredPosition = canvasPos;
        }
    }
}