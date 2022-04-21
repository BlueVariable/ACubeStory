using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public static class Extensions_Mergeable
    {
        #region Constants

        private const float DISTANCE_TOLERANCE = 0.1f;

        #endregion
        
        public static bool WillOverlapGround(this Mergeable mergeable, 
            MergeableComposition mergeableComposition, Vector3 normalizedDirection)
        {
            Vector3 mergedPos = mergeable.GetMergeablePos();
            mergedPos.Scale(normalizedDirection);
            
            Vector3 anchorPos = mergeableComposition.GetAnchorMergeable(normalizedDirection).GetMergeablePos();
            anchorPos.Scale(normalizedDirection);

            Vector3 dir = mergedPos - anchorPos;

            float normalizedDirSum = normalizedDirection.x + normalizedDirection.y + normalizedDirection.z;

            float sum = (dir.x + dir.y + dir.z) * Mathf.Sign(normalizedDirSum);

            if (Mathf.RoundToInt(sum) > 0 && Mathf.Sign(normalizedDirSum) > 0)
            {
                return true;
            }

            if (Mathf.RoundToInt(sum) < 0 && Mathf.Sign(normalizedDirSum) < 0)
            {
                return true;
            }

            return false;
        }
        
        public static bool WillOverlapInDirection(this Mergeable mergeable, 
            MergeableComposition mergeableComposition, Vector3 normalizedDirection, LayerMask mask)
        {
            Vector3 mergedPos = mergeable.GetMergeablePos();

            Vector3 anchorPos = mergeableComposition.GetAnchorMergeable(normalizedDirection).GetMergeablePos();

            bool hit = Physics.Raycast(mergedPos, normalizedDirection, 
                Mathf.CeilToInt(mergedPos.y - anchorPos.y + DISTANCE_TOLERANCE), mask);

            return hit;
        }
    }
}