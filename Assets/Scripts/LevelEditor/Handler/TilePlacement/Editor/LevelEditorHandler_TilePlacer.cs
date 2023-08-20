using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using BLUE.ACubeStory.TileSystem;

namespace BLUE.ACubeStory.LevelEditor
{
    public class LevelEditorHandler_TilePlacer : LevelEditorHandlerBase
    {
        private float _tilePlacementDistance = 10;

        private List<TilePlacementConditionBase> _placementConditions = new List<TilePlacementConditionBase>()
        {
            new TilePlacementCondition_OverlapAnotherTile(),
            new TilePlacementCondition_Cooldown()
        };
        
        private LevelEditorCommandController _commandController;
        private LevelEditorCommandController _CommandController
        {
            get
            {
                if (_commandController == null)
                {
                    _commandController = new LevelEditorCommandController(ELevelEditorCommand.TileAddition);
                }

                return _commandController;
            }
        }

        private EditorTileParentProvider _parentProvider;
        private EditorTileParentProvider _ParentProvider
        {
            get
            {
                if (_parentProvider == null)
                {
                    _parentProvider = new EditorTileParentProvider();
                }

                return _parentProvider;
            }
        }

        public double LastTilePlacementTime { get; private set; } = 0;

        public override void Handle(Event e)
        {
            Vector3 curPlacementPos = GetPlacementPosition(e.mousePosition);

            bool isPlacementValid = CanPlaceTile(curPlacementPos);
            
            Handles.color = isPlacementValid ? Color.green : Color.red;
            Handles.DrawWireCube(curPlacementPos, Vector3.one);

            bool inputRes = _CommandController.Handle(e);
            
            if (_CommandController.Handle(e) && isPlacementValid)
            {
                TryPlaceTile(curPlacementPos);

                LastTilePlacementTime = EditorApplication.timeSinceStartup;
            }
            else if (!inputRes)
            {
                LastTilePlacementTime = 0;
            } 
        }

        private Vector3 GetPlacementPosition(Vector2 mousePosition)
        {
            Vector3 placementPos;

            Ray ray = Camera.current.ScreenPointToRay(new Vector3(mousePosition.x, Camera.current.pixelHeight - mousePosition.y, 0));

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                if (hitInfo.collider.TryGetComponent(out Tile neighborCandidate))
                {
                    placementPos = neighborCandidate.transform.position + hitInfo.normal;

                    return placementPos;
                }
            }

            placementPos = ray.origin + ray.direction.normalized * _tilePlacementDistance;
            
            return placementPos;
        }

        private bool TryPlaceTile(Vector3 position)
        {
            bool res = TileFactory.Instance.TryCreateTile(LevelEditorContainer.Instance.SelectedTileID, position, out Tile tile);

            if (!res)
            {
                return false;
            }
            
            tile.transform.SetParent(_ParentProvider.GetTileParent(tile));
            
            EditorUtility.SetDirty(tile.gameObject);

            if (Selection.activeGameObject == tile.gameObject)
            {
                Selection.activeGameObject = null;
            }
            
            return true;
        }

        private bool CanPlaceTile(Vector3 position)
        {
            foreach (TilePlacementConditionBase condition in _placementConditions)
            {
                if (!condition.IsSatisfied(this, position))
                {
                    return false;
                }
            }

            return true;
        }
    }
}