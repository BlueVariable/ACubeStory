using BLUE.ACubeStory.TileSystem;

namespace BLUE.ACubeStory.LevelEditor
{
    public abstract class TileRemovalConditionBase
    {
        public abstract bool IsSatisfied(LevelEditorHandler_TileRemover tileRemover, Tile candidate);
    }
}