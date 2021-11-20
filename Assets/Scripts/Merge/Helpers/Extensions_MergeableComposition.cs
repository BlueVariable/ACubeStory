using System.Linq;
using UnityEngine;

namespace BLUE.ACubeStory.Merge
{
    public static class Extensions_MergeableComposition
    {
        #region Constants

        private const float DISTANCE_TOLERANCE = 0.1f;

        #endregion
        
        public static Mergeable GetAnchorMergeable(this MergeableComposition mergeableComposition, Vector3 normalizedDir)
        {
            Mergeable anchorMergeable = mergeableComposition.Mergeables.ElementAt(0);
            
            foreach (Mergeable mergeable in mergeableComposition.Mergeables)
            {
                if (mergeable.GetMergeablePos().y < anchorMergeable.GetMergeablePos().y - DISTANCE_TOLERANCE)
                {
                    anchorMergeable = mergeable;

                    continue;
                }
                
                if (mergeable.GetMergeablePos().y > anchorMergeable.GetMergeablePos().y + DISTANCE_TOLERANCE)
                {
                    continue;
                }
                
                Vector3 prev = anchorMergeable.GetMergeablePos();
                Vector3 cur = mergeable.GetMergeablePos();
                
                prev.Scale(normalizedDir);
                cur.Scale(normalizedDir);

                if (cur.x + cur.y + cur.z > prev.x + prev.y + prev.z)
                {
                    anchorMergeable = mergeable;
                }
            }

            return anchorMergeable;
        }
    }
}
