using BLUE.ACubeStory.LevelEditor;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class MergeNextSceneTrigger : MonoBehaviour
    {
        [SerializeField] private BoxCollider _nextSceneTriggerCollider = null;
        public BoxCollider NextSceneTriggerCollider => _nextSceneTriggerCollider;

        [SerializeField] private Transform _invisibleGroundContainer = null;
        public Transform InvisibleGroundContainer => _invisibleGroundContainer;

        [SerializeField] private Tile _invisibleTileReference = null;
        public Tile InvisibleTileReference => _invisibleTileReference;
    }
}