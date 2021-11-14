using System.Collections.Generic;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public class Merger_Raycast : MergerBase
    {
        [SerializeField] private Vector3[] _rayDirections = default;

        [SerializeField] private float _rayLength = 1f;

        [SerializeField] private LayerMask _rayHitMask = default;
        
        protected override HashSet<Mergeable> GetCandidateMergeables(Mergeable formerMergeable)
        {
            HashSet<Mergeable> candidateMergeables = new HashSet<Mergeable>();

            foreach (Vector3 rayDirection in _rayDirections)
            {
                bool rayHit = Physics.Raycast(formerMergeable.GetMergeablePos(), rayDirection, 
                    out RaycastHit hit, _rayLength, _rayHitMask);

                if (!rayHit)
                {
                    continue;
                }

                Mergeable mergeable = hit.collider.GetComponentInParent<Mergeable>();

                if (mergeable == null)
                {
                    continue;
                }

                candidateMergeables.Add(mergeable);
            }

            return candidateMergeables;
        }
    }
}