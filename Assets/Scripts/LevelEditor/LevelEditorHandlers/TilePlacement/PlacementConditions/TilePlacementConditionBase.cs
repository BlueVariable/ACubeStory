using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public abstract class TilePlacementConditionBase
    {
        public abstract bool IsSatisfied(LevelEditorHandler_TilePlacer tilePlacer, Vector3 position);
    }
}