using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class CubeLevelEditorHandler_TileRemover : CubeLevelEditorHandlerBase
    {
        private KeyCode _inputKeyCode = KeyCode.P;

        private bool _isRemoverActivated;
        
        public override void Handle(Event e)
        {
            if (e.type == EventType.KeyDown && e.keyCode == _inputKeyCode)
            {
                _isRemoverActivated = true;
            }
            
            if (e.type == EventType.KeyUp && e.keyCode == _inputKeyCode)
            {
                _isRemoverActivated = false;
            }

            TryGetTileToDelete(e.mousePosition, out Tile deletionCandidate);
            
            if (_isRemoverActivated && deletionCandidate != null)
            {
                int controlID = GUIUtility.GetControlID(FocusType.Passive);
                GUIUtility.hotControl = controlID;

                if (deletionCandidate != null)
                {
                    Handles.color = Color.magenta;
                    Handles.DrawWireCube(deletionCandidate.transform.position, Vector3.one);
                }
            }

            if (_isRemoverActivated && e.type == EventType.MouseUp && e.button == 0)
            {
                EditorUtility.SetDirty(deletionCandidate.gameObject);
                
                GameObject.DestroyImmediate(deletionCandidate.gameObject);

                deletionCandidate = null;
            }
        }

        private bool TryGetTileToDelete(Vector2 mousePosition, out Tile deletionCandidate)
        {
            deletionCandidate = null;
            
            Ray ray = Camera.current.ScreenPointToRay(new Vector3(mousePosition.x, Screen.height - mousePosition.y, 0));
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity)) 
            {
                if (hitInfo.collider.TryGetComponent(out deletionCandidate))
                {
                    return true;
                }
            }

            return false;
        }
    }
}