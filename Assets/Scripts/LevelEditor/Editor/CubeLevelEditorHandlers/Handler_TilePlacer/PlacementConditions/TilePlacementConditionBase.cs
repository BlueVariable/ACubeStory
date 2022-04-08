using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public abstract class TilePlacementConditionBase
    {
        public abstract bool IsSatisfied(Vector3 position);
    }
}