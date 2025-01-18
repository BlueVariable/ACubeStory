using System.Collections.Generic;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class DirectionProvider_LastRollDirection : DirectionProviderBase
    {
        [SerializeField] private State_Cube_Roll _rollState = null;
        
        public override EDirection GetDirection()
        {
            foreach (KeyValuePair<EDirection, Vector3> directionMap in DirectionUtility.DirectionMap)
            {
                if (directionMap.Value == _rollState.LastCandidateTransitionMessage.Direction)
                {
                    return directionMap.Key;
                }
            }

            return EDirection.None;
        }
    }
}