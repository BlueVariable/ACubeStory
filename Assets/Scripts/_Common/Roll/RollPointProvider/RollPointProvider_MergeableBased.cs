using System;
using System.Collections;
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

        private const float HEIGHT_TOLERANCE = 0.05f;

        #endregion
        
        [SerializeField] private Settings[] _settings = new Settings[0];
        
        [SerializeField] private MergeableComposition _mergeableComposition = null;

        private IEnumerator _checkMergeableHitGroundRoutine;
        
        public override Vector3 GetRollPoint(Vector3 direction)
        {
            Mergeable targetMergeable = _mergeableComposition.GetAnchorMergeable(direction.normalized);

            Vector3 rollPoint = targetMergeable.GetMergeablePos() 
                                + _settings.FirstOrDefault(i => i.Direction.Equals(direction)).Offset;

            return rollPoint;
        }
    }
}