using UnityEngine;

namespace BLUE.ACubeStory.Grass
{
    public class GrassSmasher_Cube : MonoBehaviour
    {
        [SerializeField] private Transform _cubeRoot = null;
        
        [SerializeField] private Vector3 _smasherPositionOffset = new Vector3(0, 0.5f, 0);
        
        private void Update()
        {
            Vector3 pos = _cubeRoot.position;

            pos -= _smasherPositionOffset;

            transform.position = pos;
        }

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent(out Grass grass);

            if (grass == null)
            {
                return;
            }
            
            grass.SmashGrass();
        }

        private void OnTriggerExit(Collider other)
        {
            other.TryGetComponent(out Grass grass);

            if (grass == null)
            {
                return;
            }
            
            grass.RecoveryGrass(true);
        }
    }
}