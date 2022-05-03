using System;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public abstract class FallingObjectBase : MonoBehaviour
    {
        public abstract Action<FallingObjectBase> OnCompleted { get; set; }
        public abstract void Fall(Vector3 fallPosition);
    }
}