using System.Collections.Generic;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class DirectionProvider_CubeLastDescendDirection : DirectionProviderBase
    {
        [SerializeField] private State_Cube_Descend _descendState = null;
        
        public override EDirection GetDirection()
        {
            foreach (KeyValuePair<EDirection, Vector3> directionMap in DirectionHelper.DirectionVectorMapping)
            {
                if (directionMap.Value == _descendState.LastCandidateTransitionMessage.Direction)
                {
                    return directionMap.Key;
                }
            }

            return EDirection.None;
        }
    }
}