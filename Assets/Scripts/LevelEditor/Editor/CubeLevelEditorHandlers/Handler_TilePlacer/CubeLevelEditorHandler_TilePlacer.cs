using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class CubeLevelEditorHandler_TilePlacer : CubeLevelEditorHandlerBase
    {
        private float _tilePlacementDistance = 10;
        
        private KeyCode _inputKeyCode = KeyCode.O;

        private bool _isPlacerActivated;
        
        private List<TilePlacementConditionBase> _placementConditions = new List<TilePlacementConditionBase>()
        {
            new TilePlacementCondition_OverlapAnotherTile()
        };

        public override void Handle(Event e)
        {
            if (e.type == EventType.KeyDown && e.keyCode == _inputKeyCode)
            {
                _isPlacerActivated = true;
            }
            
            if (e.type == EventType.KeyUp && e.keyCode == _inputKeyCode)
            {
                _isPlacerActivated = false;
            }

            Vector3 curPlacementPos = GetPlacementPosition(e.mousePosition);

            bool isPlacementValid = CanPlaceTile(curPlacementPos);

            if (_isPlacerActivated)
            {
                Handles.color = isPlacementValid ? Color.green : Color.red;
                
                Handles.DrawWireCube(curPlacementPos, Vector3.one);
            }

            if (e.type == EventType.MouseDown && e.button == 0)
            {
                
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
                    placementPos = neighborCandidate.transform.position + hitInfo.normal;
                    
                    return placementPos;
                }
            }

            placementPos = ray.origin + ray.direction.normalized * _tilePlacementDistance;
            
            return placementPos;
        }

        private bool TryPlaceTile(Vector3 position)
        {
            
            
            return true;
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