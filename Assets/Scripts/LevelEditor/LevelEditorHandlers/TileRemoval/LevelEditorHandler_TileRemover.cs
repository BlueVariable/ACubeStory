using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using BLUE.ACubeStory.TileSystem;

namespace BLUE.ACubeStory.LevelEditor
{
    public class LevelEditorHandler_TileRemover : LevelEditorHandlerBase
    {
        private List<TileRemovalConditionBase> _removalConditions = new List<TileRemovalConditionBase>()
        {
            new TileRemovalCondition_IsTileValid(),
            new TileRemovalCondition_Cooldown()
        };
        
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

        public double LastTileRemovalTime { get; private set; } = 0;

        public override void Handle(Event e)
        {
            TryGetTileToRemove(e.mousePosition, out Tile deletionCandidate);

            bool input = _InputHandler.Handle(e);

            bool canRemoveTile = CanRemoveTile(deletionCandidate);

            if (canRemoveTile)
            {
                Handles.color = Color.magenta;
                Handles.DrawWireCube(deletionCandidate.transform.position, Vector3.one);
                
                if (input)
                {
                    EditorUtility.SetDirty(deletionCandidate.gameObject);
                
                    Object.DestroyImmediate(deletionCandidate.gameObject);

                    deletionCandidate = null;
                    
                    LastTileRemovalTime = EditorApplication.timeSinceStartup;
                }
                else
                {
                    LastTileRemovalTime = 0;
                }
            }
        }

        private bool TryGetTileToRemove(Vector2 mousePosition, out Tile removalCandidate)
        {
            removalCandidate = null;
            
            Ray ray = Camera.current.ScreenPointToRay(new Vector3(mousePosition.x, Camera.current.pixelHeight - mousePosition.y, 0));
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity)) 
            {
                if (hitInfo.collider.TryGetComponent(out removalCandidate))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CanRemoveTile(Tile tile)
        {
            foreach (TileRemovalConditionBase condition in _removalConditions)
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