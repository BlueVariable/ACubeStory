using UnityEditor;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TileRemovingCondition_Cooldown : TileRemovingConditionBase
    {
        public override bool IsSatisfied(CubeLevelEditorHandler_TileRemover tileRemover, Tile candidate)
        {
            return EditorApplication.timeSinceStartup - tileRemover.LastTilePlacementTime > 0.25f;
        }
    }
}