using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TilePlacementCondition_Cooldown : TilePlacementConditionBase
    {
        #region Constants

        private const float COOLDOWN = 0.25f;

        #endregion
        
        public override bool IsSatisfied(CubeLevelEditorHandler_TilePlacer tilePlacer, Vector3 position)
        {
            return EditorApplication.timeSinceStartup - tilePlacer.LastTilePlacementTime > COOLDOWN;
        }
    }
}