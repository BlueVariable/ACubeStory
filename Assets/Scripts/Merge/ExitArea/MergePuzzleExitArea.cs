using BLUE.ACubeStory.TileSystem;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class MergePuzzleExitArea : MonoBehaviour
    {
        [SerializeField] private BoxCollider _nextSceneTriggerCollider = null;
        public BoxCollider NextSceneTriggerCollider => _nextSceneTriggerCollider;

        [SerializeField] private Transform _invisibleGroundContainer = null;
        public Transform InvisibleGroundContainer => _invisibleGroundContainer;

        [SerializeField] private Tile _invisibleTileReference = null;
        public Tile InvisibleTileReference => _invisibleTileReference;
        
        [field: SerializeField] public Transform MergableSlotGrounds { get; private set; }
    }
}