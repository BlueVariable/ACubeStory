namespace BLUE.ACubeStory.LevelEditor
{
    public abstract class TileRemovingConditionBase
    {
        public abstract bool IsSatisfied(CubeLevelEditorHandler_TileRemover tileRemover, Tile candidate);
    }
}