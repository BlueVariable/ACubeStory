using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public class DirectionProvider_Cube : DirectionProviderBase
    {
        [SerializeField] private EDirection _worldDirection = EDirection.None;

        public override EDirection GetDirection()
        {
            return CubeDirectionUtility.ConvertToCubesLocalDirection(_worldDirection);
        }
    }
}