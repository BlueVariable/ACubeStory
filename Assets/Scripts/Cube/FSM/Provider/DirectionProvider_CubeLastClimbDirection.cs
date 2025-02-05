using System.Collections.Generic;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class DirectionProvider_CubeLastClimbDirection : DirectionProviderBase
    {
        [SerializeField] private State_Cube_Descend _climbState = null;
        
        public override EDirection GetDirection()
        {
            foreach (KeyValuePair<EDirection, Vector3> directionMap in DirectionHelper.DirectionVectorMapping)
            {
                if (directionMap.Value == _climbState.LastCandidateTransitionMessage.Direction)
                {
                    return directionMap.Key;
                }
            }

            return EDirection.None;
        }
    }
}