using System;
using System.Linq;
using UnityEngine;

namespace BLUE.ACubeStory.Behaviours
{
    public abstract class RollBehaviourBase : MonoBehaviour
    {
        public enum ERollDirection
        {
            None = 0,
            Left = 1,
            Right = 2,
            Forward = 3,
            Backward = 4
        }
        
        [SerializeField] private RollConditionBase[] _rollConditions = null;

        public bool IsRolling { get; private set; }
        
        public Action OnRollStarted { get; set; }
        public Action OnRollEnded { get; set; }
        
        public bool TryRollTo(ERollDirection direction, Action onRolledCallback = null)
        {
            if (!CanRoll(direction))
            {
                return false;
            }

            IsRolling = true;
            
            OnRollStarted?.Invoke();

            Roll(direction, onRollEnded);

            void onRollEnded()
            {
                IsRolling = false;
                
                OnRollEnded?.Invoke();

                onRolledCallback?.Invoke();
            }

            return true;
        }

        public abstract void StopRoll();

        protected abstract void Roll(ERollDirection direction, Action onRolledCallback = null);

        protected Vector3 GetRollAxis(ERollDirection direction)
        {
            switch (direction)
            {
                case ERollDirection.Left: return Vector3.forward;
                case ERollDirection.Right: return Vector3.back;
                case ERollDirection.Forward: return Vector3.right;
                case ERollDirection.Backward: return Vector3.left;
            }

            return default;
        }

        private bool CanRoll(ERollDirection direction)
        {
            return !IsRolling && _rollConditions.All(i => i.CanRoll(direction));
        }
    }
}