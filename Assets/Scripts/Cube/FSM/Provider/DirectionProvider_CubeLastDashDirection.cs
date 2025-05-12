using System.Collections.Generic;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class DirectionProvider_CubeLastDashDirection : DirectionProviderBase
    {
        [SerializeField] private State_Cube_Dash _dashState = null;
        
        public override EDirection GetDirection()
        {
            foreach (KeyValuePair<EDirection, Vector3> directionMap in DirectionHelper.DirectionVectorMapping)
            {
                if (directionMap.Value == _dashState.LastCandidateTransitionMessage.DashDirection)
                {
                    return directionMap.Key;
                }
            }

            return EDirection.None;
        }
    }
}