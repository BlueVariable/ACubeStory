using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public static class CubeDirectionHelper
    {
        #region Constants

        private const float COMPARISON_VALUE = 0.9f;

        #endregion
        
        public static EDirection ConvertToCubesLocalDirection(EDirection worldDirection)
        {
            Vector3 direction = DirectionHelper.DirectionVectorMapping[worldDirection];
            
            if (Vector3.Dot(direction, Cube.Instance.transform.up) > COMPARISON_VALUE)
                return EDirection.Up;
            if (Vector3.Dot(direction, -Cube.Instance.transform.up) > COMPARISON_VALUE)
                return EDirection.Down;
            if (Vector3.Dot(direction, Cube.Instance.transform.right) > COMPARISON_VALUE)
                return EDirection.Right;
            if (Vector3.Dot(direction, -Cube.Instance.transform.right) > COMPARISON_VALUE)
                return EDirection.Left;
            if (Vector3.Dot(direction, Cube.Instance.transform.forward) > COMPARISON_VALUE)
                return EDirection.Forward;
            if (Vector3.Dot(direction, -Cube.Instance.transform.forward) > COMPARISON_VALUE)
                return EDirection.Backward;

            return EDirection.None;
        }
    }
}