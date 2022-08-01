using BLUE.ACubeStory.TileSystem;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TileRemovalCondition_IsTileValid : TileRemovalConditionBase
    {
        public override bool IsSatisfied(LevelEditorHandler_TileRemover tileRemover, Tile candidate)
        {
            return candidate != null;
        }
    }
}