using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Cube
{
    public static class CubeDirectionUtility
    {
        public static EDirection ConvertToCubesLocalDirection(EDirection worldDirection)
        {
            Vector3 direction = DirectionUtility.DirectionMap[worldDirection];
            
            if (Vector3.Dot(direction, Cube.Instance.transform.up) > 0.9)
                return EDirection.Up;
            if (Vector3.Dot(direction, -Cube.Instance.transform.up) > 0.9)
                return EDirection.Down;
            if (Vector3.Dot(direction, Cube.Instance.transform.right) > 0.9)
                return EDirection.Right;
            if (Vector3.Dot(direction, -Cube.Instance.transform.right) > 0.9)
                return EDirection.Left;
            if (Vector3.Dot(direction, Cube.Instance.transform.forward) > 0.9)
                return EDirection.Forward;
            if (Vector3.Dot(direction, -Cube.Instance.transform.forward) > 0.9)
                return EDirection.Backward;

            return EDirection.None;
        }
    }
}