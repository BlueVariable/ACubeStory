using UnityEditor;
using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public class TilePlacementCondition_Cooldown : TilePlacementConditionBase
    {
        public override bool IsSatisfied(CubeLevelEditorHandler_TilePlacer tilePlacer, Vector3 position)
        {
            return EditorApplication.timeSinceStartup - tilePlacer.LastTilePlacementTime > 0.75f;
        }
    }
}