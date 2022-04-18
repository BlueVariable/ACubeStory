using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TilePlacementCondition_OverlapAnotherTile : TilePlacementConditionBase
    {
        private float _overlapBoxHalfExtent = 0.5f;
        
        public override bool IsSatisfied(CubeLevelEditorHandler_TilePlacer tilePlacer, Vector3 position)
        {
            Collider[] overlaps = Physics.OverlapBox(position, Vector3.one * _overlapBoxHalfExtent);

            foreach (Collider overlap in overlaps)
            {
                if (overlap.TryGetComponent(out Tile tile))
                {
                    return false;
                }
            }

            return true;
        }
    }
}