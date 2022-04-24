using UnityEngine;

namespace BLUE.ACubeStory.Grass
{
    public class GrassTrampler : MonoBehaviour
    {
        [SerializeField] private Transform _root = null;
        
        [SerializeField] private Vector3 _tramplerPositionOffset = new Vector3(0, 0.5f, 0);
        
        private void Update()
        {
            Vector3 pos = _root.position;

            pos -= _tramplerPositionOffset;

            transform.position = pos;
        }

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent(out Grass grass);

            if (grass == null)
            {
                return;
            }
            
            grass.TrampleGrass();
        }

        private void OnTriggerExit(Collider other)
        {
            other.TryGetComponent(out Grass grass);

            if (grass == null)
            {
                return;
            }
            
            grass.RecoverGrass(true);
        }
    }
}