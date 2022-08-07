using BLUE.ACubeStory.TileSystem;
using UnityEditor;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TileRemovalCondition_Cooldown : TileRemovalConditionBase
    {
        #region Constants

        private const float COOLDOWN = 0.25f;

        #endregion
        
        public override bool IsSatisfied(LevelEditorHandler_TileRemover tileRemover, Tile candidate)
        {
            return EditorApplication.timeSinceStartup - tileRemover.LastTileRemovalTime > COOLDOWN;
        }
    }
}