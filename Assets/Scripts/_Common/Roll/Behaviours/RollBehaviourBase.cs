using System;
using System.Linq;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public abstract class RollBehaviourBase : MonoBehaviour
    {
        [SerializeField] private RollConditionBase[] _rollConditions = null;

        public bool IsRolling { get; private set; }
        public bool IsRollPaused { get; private set; }
        
        public float CurRollAngle { get; protected set; }

        #region Events

        public Action OnRollStarted { get; set; }
        public Action OnRollEnded { get; set; }
        public Action OnRollPaused { get; set; }

        #endregion

        protected abstract void PauseRoll();

        protected abstract void StartRoll(
            Vector3 direction, 
            float angle = 90f,
            float speed = 5f,
            Vector3 rollPointOffset = default,
            AnimationCurve rollCurve = default,
            Action onRolledCallback = null);

        public bool TryPauseRoll()
        {
            if (!IsRolling)
            {
                return false;
            }

            IsRolling = false;
            
            OnRollPaused?.Invoke();
            
            PauseRoll();

            IsRollPaused = true;
            
            return true;
        }
        
        public bool TryRollTo(
            Vector3 direction,
            float angle = 90f,
            float speed = 5f,
            Vector3 rollPointOffset = default,
            AnimationCurve rollCurve = default,
            Action onRolledCallback = null)
        {
            if (!CanRoll(direction))
            {
                return false;
            }

            IsRolling = true;
            
            OnRollStarted?.Invoke();

            StartRoll(direction, angle, speed, rollPointOffset, rollCurve, onRollEnded);

            void onRollEnded()
            {
                IsRolling = false;

                IsRollPaused = false;
                
                OnRollEnded?.Invoke();

                onRolledCallback?.Invoke();
            }

            return true;
        }

        protected Vector3 GetRollAxis(Vector3 direction)
        {
            if (direction == Vector3.left)
            {
                return Vector3.forward;
            }
            else if (direction == Vector3.right)
            {
                return Vector3.back;
            }
            else if (direction == Vector3.forward)
            {
                return Vector3.right;
            }
            else if (direction == Vector3.back)
            {
                return Vector3.left;
            }
            
            return default;
        }

        private bool CanRoll(Vector3 direction)
        {
            return !IsRolling && _rollConditions.All(i => i.CanRoll(direction));
        }
    }
}