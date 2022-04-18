using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public abstract class TilePlacementConditionBase
    {
        public abstract bool IsSatisfied(CubeLevelEditorHandler_TilePlacer tilePlacer, Vector3 position);
    }
}