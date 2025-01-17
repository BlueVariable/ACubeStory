using System;
using BLUE.Common;
using MegaFiers;
using UnityEngine;

namespace BLUE.ACubeStory.MegaFiers
{
    public class MegaModifierProvider_DirectionBased : MegaModifierProviderBase
    {
        [SerializeField] private DirectionProviderBase _directionProvider = null;
        
        [SerializeField] private DirectionModifierInfo[] _directionModifierMap = null;
        
        [Serializable]
        public class DirectionModifierInfo
        {
            [field: SerializeField] public EDirection Direction { get; private set; }
            [field: SerializeField] public MegaModifier Modifier { get; private set; }
        }
        
        public override MegaModifier GetModifier()
        {
            EDirection direction = _directionProvider.GetDirection();

            foreach (DirectionModifierInfo info in _directionModifierMap)
            {
                if (info.Direction == direction)
                {
                    return info.Modifier;
                }
            }

            return null;
        }
    }
}