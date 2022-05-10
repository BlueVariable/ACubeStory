using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class MergeAreaWall : MonoBehaviour
    {
        [SerializeField] private Collider _collider = null;
        public Collider Collider => _collider;

        [SerializeField] private Renderer _renderer = null;
        public Renderer Renderer => _renderer;
    }
}