using UnityEngine;
using System.Collections.Generic;

namespace BLUE.ACubeStory.Platform
{
    public class PlatformEntrancePoint_CubeOverlapBox : PlatformEntrancePoint
    {
        [SerializeField] private Vector3 _positionOffset = Vector3.zero;
        
        [SerializeField] private Vector3 _overlapBoxHalfExtents = Vector3.zero;
        
        [SerializeField] private LayerMask _layerMask = default;

        public override List<Transform> GetCurrentObjectsOnPlatform()
        {
            Collider[] overlappedColliders =
                Physics.OverlapBox(transform.position + _positionOffset, _overlapBoxHalfExtents, Quaternion.identity, _layerMask);
            
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
            
            Gizmos.DrawWireCube(transform.position + _positionOffset, _overlapBoxHalfExtents * 2);
        }
#endif
    }
}