using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.Platform
{
    public class PlatformEntrancePoint_CubeSphereCast : PlatformEntrancePoint
    {
        [SerializeField] private float _sphereCastRadius = 0.5f;

        [SerializeField] private Vector3 _sphereCastLocalPosition = Vector3.zero;

        [SerializeField] private LayerMask _layerMask = default;

        public override List<Transform> GetCurrentObjectsOnPlatform()
        {
            Collider[] overlappedColliders =
                Physics.OverlapSphere(transform.position + _sphereCastLocalPosition, _sphereCastRadius, _layerMask);
            
            List<Transform> currentObjects = new List<Transform>();

            foreach (Collider overlappedCollider in overlappedColliders)
            {
                if (overlappedCollider.TryGetComponent(out Cube.Cube cube))
                {
                    currentObjects.Add(overlappedCollider.transform);
                }
            }

            return currentObjects;
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            
            Gizmos.DrawWireSphere(transform.position + _sphereCastLocalPosition, _sphereCastRadius);            
        }
#endif
    }
}