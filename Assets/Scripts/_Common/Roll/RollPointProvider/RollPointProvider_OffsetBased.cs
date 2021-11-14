using System;
using System.Linq;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class RollPointProvider_OffsetBased : RollPointProviderBase
    {
        [Serializable]
        private class Settings
        {
            [SerializeField] private Vector3 _direction = Vector3.zero;
            public Vector3 Direction => _direction;

            [SerializeField] private Vector3 _offset = default;
            public Vector3 Offset => _offset;
        }
        
        [SerializeField] private Settings[] _settings = new Settings[0];

        [SerializeField] private Transform _baseTransform = null;

        public override Vector3 GetRollPoint(Vector3 direction)
        {
            return _baseTransform.position + _settings.FirstOrDefault(i => i.Direction.Equals(direction)).Offset;
        }
    }
}