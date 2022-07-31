using BLUE.ACubeStory.TileSystem;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TilePlacementCondition_OverlapAnotherTile : TilePlacementConditionBase
    {
        #region Constants

        private const float OVERLAP_BOX_HALF_EXTENTS = 0.45f;

        #endregion

        public override bool IsSatisfied(CubeLevelEditorHandler_TilePlacer tilePlacer, Vector3 position)
        {
            Collider[] overlaps = Physics.OverlapBox(position, Vector3.one * OVERLAP_BOX_HALF_EXTENTS);

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