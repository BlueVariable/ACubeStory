using System;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public abstract class MovingObjectBase : MonoBehaviour
    {
        #region Events

        public abstract Action<MovingObjectBase> OnSequenceCompleted { get; set; }

        #endregion
        
        public abstract void Move(Vector3 targetPosition);
    }
}