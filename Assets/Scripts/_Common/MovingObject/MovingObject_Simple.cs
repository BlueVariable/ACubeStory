using System;
using BLUE.ActionSystem;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class MovingObject_Simple : MovingObjectBase
    {
        [SerializeField] private MoveBehaviour_Tween _moveBehaviour = null;
        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _moveSettings = null;
        [SerializeField] private ActionExecutor _reachedTargetActions = null;

        [SerializeField] private bool _manageGameObjectActivation = true;
        
        #region Events

        public override Action<MovingObjectBase> OnSequenceCompleted { get; set; }

        #endregion
        
        public override void Move(Vector3 targetPosition)
        {
            if (_manageGameObjectActivation)
            {
                gameObject.SetActive(true);
            }

            _moveBehaviour.StartMovementTowards(_moveSettings, targetPosition, onReachedTarget);

            void onReachedTarget()
            {
                _reachedTargetActions.OnExecutionCompleted += onReachedTargetActionsCompleted;
                _reachedTargetActions.Execute();
            }

            void onReachedTargetActionsCompleted()
            {
                _reachedTargetActions.OnExecutionCompleted -= onReachedTargetActionsCompleted;

                if (_manageGameObjectActivation)
                {
                    gameObject.SetActive(false);
                }

                OnSequenceCompleted?.Invoke(this);
            }
        }
    }
}