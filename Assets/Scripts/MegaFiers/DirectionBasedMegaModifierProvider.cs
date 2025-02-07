using System;
using BLUE.Common;
using MegaFiers;
using UnityEngine;

namespace BLUE.ACubeStory.MegaFiers
{
    public class DirectionBasedMegaModifierProvider : MonoBehaviour
    {
        [Serializable]
        public class DirectionModifierInfo
        {
            [field: SerializeField] public EDirection Direction { get; private set; }
            [field: SerializeField] public MegaModifier Modifier { get; private set; }
        }
        
        [SerializeField] private DirectionModifierInfo[] _directionModifierMap = null;
        public DirectionModifierInfo[] DirectionModifierMapColl => _directionModifierMap;
        
        public bool TryGetModifier(EDirection direction, out MegaModifier modifier)
        {
            modifier = null;
            
            foreach (DirectionModifierInfo info in _directionModifierMap)
            {
                if (info.Direction == direction)
                {
                    modifier = info.Modifier;
                    
                    return true;
                }
            }

            return false;
        }
    }
}