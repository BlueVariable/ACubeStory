using System;
using System.Linq;
using BLUE.ACubeStory.Merge;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class RollPointProvider_MergeableBased : RollPointProviderBase
    {
        [Serializable]
        private class Settings
        {
            [SerializeField] private Vector3 _direction = Vector3.zero;
            public Vector3 Direction => _direction;

            [SerializeField] private Vector3 _offset = default;
            public Vector3 Offset => _offset;
        }
        
        #region Constants

        private const float HEIGHT_INEQUALITY_TOLERANCE = 0.1f;

        #endregion
        
        [SerializeField] private MergeableComposition _mergeableComposition = null;
        
        [SerializeField] private Settings[] _settings = new Settings[0];
        
        public override Vector3 GetRollPoint(Vector3 direction)
        {
            Mergeable targetMergeable = _mergeableComposition.Mergeables.ElementAt(0);

            foreach (Mergeable mergeable in _mergeableComposition.Mergeables)
            {
                if (mergeable.GetMergeablePos().y < targetMergeable.GetMergeablePos().y - HEIGHT_INEQUALITY_TOLERANCE)
                {
                    targetMergeable = mergeable;

                    continue;
                }

                if (mergeable.GetMergeablePos().y > targetMergeable.GetMergeablePos().y + HEIGHT_INEQUALITY_TOLERANCE)
                {
                    continue;
                }
                
                Vector3 prev = targetMergeable.GetMergeablePos();
                Vector3 cur = mergeable.GetMergeablePos();
                
                prev.Scale(direction);
                cur.Scale(direction);

                if (cur.x + cur.y + cur.z > prev.x + prev.y + prev.z)
                {
                    targetMergeable = mergeable;
                }
            }

            Vector3 rollPoint = targetMergeable.GetMergeablePos() + _settings.FirstOrDefault(i
                => i.Direction.Equals(direction)).Offset;

            return rollPoint;
        }
    }
}