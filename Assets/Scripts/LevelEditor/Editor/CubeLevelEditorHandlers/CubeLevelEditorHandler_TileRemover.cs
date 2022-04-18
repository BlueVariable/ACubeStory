using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class CubeLevelEditorHandler_TileRemover : CubeLevelEditorHandlerBase
    {
        private CubeEditorInputHandler _inputHandler;

        private CubeEditorInputHandler _InputHandler
        {
            get
            {
                if (_inputHandler == null)
                {
                    _inputHandler = new CubeEditorInputHandler(ECubeEditorControlType.TileRemove);
                }

                return _inputHandler;
            }
        }
        
        public override void Handle(Event e)
        {
            TryGetTileToDelete(e.mousePosition, out Tile deletionCandidate);
            
            if (deletionCandidate != null)
            {
                if (deletionCandidate != null)
                {
                    Handles.color = Color.magenta;
                    Handles.DrawWireCube(deletionCandidate.transform.position, Vector3.one);
                }
                
                if (_InputHandler.Handle(e))
                {
                    EditorUtility.SetDirty(deletionCandidate.gameObject);
                
                    GameObject.DestroyImmediate(deletionCandidate.gameObject);

                    deletionCandidate = null;
                }
            }
        }

        private bool TryGetTileToDelete(Vector2 mousePosition, out Tile deletionCandidate)
        {
            deletionCandidate = null;
            
            Ray ray = Camera.current.ScreenPointToRay(new Vector3(mousePosition.x, Camera.current.pixelHeight - mousePosition.y, 0));
            
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