using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TilePlacementCondition_Cooldown : TilePlacementConditionBase
    {
        #region Constants

        private const float COOLDOWN = 0.1f;

        #endregion
        
        public override bool IsSatisfied(LevelEditorHandler_TilePlacer tilePlacer, Vector3 position)
        {
            return EditorApplication.timeSinceStartup - tilePlacer.LastTilePlacementTime > COOLDOWN;
        }
    }
}