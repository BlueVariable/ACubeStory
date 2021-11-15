using System;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public interface IRollPointProvider
    {
        public Vector3 GetRollPoint(Vector3 direction, Action<Vector3> onRollPointUpdatedCallback = null);
    }
}