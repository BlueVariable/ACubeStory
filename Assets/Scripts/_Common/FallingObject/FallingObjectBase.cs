using System;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public abstract class FallingObjectBase : MonoBehaviour
    {
        #region Events

        public abstract Action<FallingObjectBase> OnCompleted { get; set; }

        #endregion
        
        public abstract void Fall(Vector3 fallPosition);
    }
}