using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class CubeLevelEditorHandler_TilePlacer : CubeLevelEditorHandlerBase
    {
        private float _tilePlacementDistance = 10;

        private List<TilePlacementConditionBase> _placementConditions = new List<TilePlacementConditionBase>()
        {
            new TilePlacementCondition_OverlapAnotherTile()
        };

        public override void Handle(Event e)
        {
            Vector3 curPlacementPos = GetPlacementPosition(e.mousePosition);

            bool isPlacementValid = CanPlaceTile(curPlacementPos);
            
            Handles.color = isPlacementValid ? Color.green : Color.red;
            Handles.DrawWireCube(curPlacementPos, Vector3.one);

            if (e.type == EventType.MouseDown && e.button == 0 && isPlacementValid)
            {
                TryPlaceTile(curPlacementPos);
            }
        }

        private Vector3 GetPlacementPosition(Vector2 mousePosition)
        {
            Vector3 placementPos;
            
            Ray ray = Camera.current.ScreenPointToRay(new Vector3(mousePosition.x, Screen.height - mousePosition.y, 0));
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity)) 
            {
                if (hitInfo.collider.TryGetComponent(out Tile neighborCandidate))
                {
                    placementPos = neighborCandidate.transform.position + hitInfo.normal * 1.0001f;
                    
                    return placementPos;
                }
            }

            placementPos = ray.origin + ray.direction.normalized * _tilePlacementDistance;
            
            return placementPos;
        }

        private bool TryPlaceTile(Vector3 position)
        {
            bool res = TileFactory.Instance.TryCreateTile(CubeEditor.Instance.SelectedTileID, position, out Tile tile);

            if (!res)
            {
                return false;
            }
            
            tile.transform.SetParent(CubeEditor.Instance.SelectedTileGroup.transform);
            
            EditorUtility.SetDirty(tile.gameObject);

            if (Selection.activeGameObject == tile.gameObject)
            {
                Selection.activeGameObject = null;
            }
            
            return res;
        }

        private bool CanPlaceTile(Vector3 position)
        {
            foreach (TilePlacementConditionBase condition in _placementConditions)
            {
                if (!condition.IsSatisfied(position))
                {
                    return false;
                }
            }

            return true;
        }
    }
}