using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using BLUE.ACubeStory.TileSystem;

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
        
        private List<TileRemovalConditionBase> _removingConditions = new List<TileRemovalConditionBase>()
        {
            new TileRemovalCondition_IsTileValid(),
            new TileRemovalCondition_Cooldown()
        };
        
        public double LastTilePlacementTime = 0;
        
        public override void Handle(Event e)
        {
            TryGetTileToDelete(e.mousePosition, out Tile deletionCandidate);

            bool input = _InputHandler.Handle(e);

            bool canRemoveTile = CanRemoveTile(deletionCandidate);

            if (canRemoveTile)
            {
                Handles.color = Color.magenta;
                Handles.DrawWireCube(deletionCandidate.transform.position, Vector3.one);
                
                if (input)
                {
                    EditorUtility.SetDirty(deletionCandidate.gameObject);
                
                    GameObject.DestroyImmediate(deletionCandidate.gameObject);

                    deletionCandidate = null;
                    
                    LastTilePlacementTime = EditorApplication.timeSinceStartup;
                }
                else
                {
                    LastTilePlacementTime = 0;
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

        private bool CanRemoveTile(Tile tile)
        {
            foreach (TileRemovalConditionBase condition in _removingConditions)
            {
                if (!condition.IsSatisfied(this, tile))
                {
                    return false;
                }
            }

            return true;
        }
    }
}