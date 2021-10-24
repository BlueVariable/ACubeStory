using System;
using System.Linq;
using UnityEngine;

namespace BLUE.ACubeStory.Behaviours
{
    public class RollPointProvider_OffsetBased : RollPointProviderBase
    {
        [SerializeField] private Settings[] _settings = new Settings[0];

        [SerializeField] private Transform _baseTransform = null;
        
        [Serializable]
        public class Settings
        {
            [SerializeField] private RollBehaviourBase.ERollDirection _direction = RollBehaviourBase.ERollDirection.None;
            public RollBehaviourBase.ERollDirection Direction => _direction;

            [SerializeField] private Vector3 _offset = default;
            public Vector3 Offset => _offset;
        }
        
        public override Vector3 GetRollPoint(RollBehaviourBase.ERollDirection direction)
        {
            return _baseTransform.position + _settings.FirstOrDefault(i => i.Direction == direction).Offset;
        }
    }
}