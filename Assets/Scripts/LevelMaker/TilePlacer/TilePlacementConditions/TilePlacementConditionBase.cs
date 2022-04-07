using UnityEngine;

namespace BLUE.ACubeStory.LevelEditor
{
    public abstract class TilePlacementConditionBase : MonoBehaviour
    {
        public abstract bool IsSatisfied(Vector3 position);
    }
}