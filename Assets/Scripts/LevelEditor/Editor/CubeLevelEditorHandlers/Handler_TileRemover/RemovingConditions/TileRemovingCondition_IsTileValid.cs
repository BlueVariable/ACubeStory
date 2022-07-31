using BLUE.ACubeStory.TileSystem;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TileRemovingCondition_IsTileValid : TileRemovingConditionBase
    {
        public override bool IsSatisfied(CubeLevelEditorHandler_TileRemover tileRemover, Tile candidate)
        {
            return candidate != null;
        }
    }
}