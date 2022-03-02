using System;
using System.Linq;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public abstract class RollBehaviourBase : MonoBehaviour
    {
        #region Constants

        private const float DEFAULT_ANGLE = 90f;
        private const float DEFAULT_SPEED = 5f;

        #endregion
        
        [SerializeField] private RollConditionBase[] _rollConditions = null;

        public bool IsRolling { get; private set; }
        
        public float CurRollAngle { get; protected set; }
        
        public Vector3 LastRollDirection { get; private set; }

        #region Events

        public Action OnRollStarted { get; set; }
        public Action OnRollEnded { get; set; }
        public Action OnRollPaused { get; set; }

        #endregion

        protected abstract void StartRoll(
            Vector3 direction, 
            float angle = DEFAULT_ANGLE,
            float speed = DEFAULT_SPEED,
            Vector3 rollPointOffset = default,
            AnimationCurve rollCurve = default,
            Action onRolledCallback = null);
        
        protected abstract void PauseRoll();

        protected abstract void UndoRoll(Action onRolledCallback = null);

        public bool TryRollTo(
            Vector3 direction,
            float angle = DEFAULT_ANGLE,
            float speed = DEFAULT_SPEED,
            Vector3 rollPointOffset = default,
            AnimationCurve rollCurve = default,
            Action onRolledCallback = null)
        {
            if (!CanRoll(direction))
            {
                return false;
            }

            LastRollDirection = direction;
                
            IsRolling = true;
            
            OnRollStarted?.Invoke();

            StartRoll(direction, angle, speed, rollPointOffset, rollCurve, onRollEnded);

            void onRollEnded()
            {
                IsRolling = false;
                
                OnRollEnded?.Invoke();

                onRolledCallback?.Invoke();
            }

            return true;
        }
        
        public bool TryPauseRoll()
        {
            if (!IsRolling)
            {
                return false;
            }

            IsRolling = false;
            
            OnRollPaused?.Invoke();
            
            PauseRoll();
            
            return true;
        }

        public bool TryUndoRoll(bool forceUndo = false, Action onRolledCallback = null)
        {
            if (!IsRolling && !forceUndo)
            {
                return false;
            }
            
            UndoRoll(onRollEnded);

            void onRollEnded()
            {
                IsRolling = false;
                
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