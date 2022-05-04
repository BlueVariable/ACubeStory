using System;
using BLUE.ActionSystem;
using BLUE.Common;
using UnityEngine;

namespace BLUE.ACubeStory.Common
{
    public class FallingObject_Rain : FallingObjectBase
    {
        [SerializeField] private MoveBehaviour_Tween _moveBehaviour = null;
        [SerializeField] private MoveBehaviour_Tween.TweenMoveSettings _moveSettings = null;
        [SerializeField] private ActionExecutor _reachedTargetActions = null;

        #region Events

        public override Action<FallingObjectBase> OnCompleted { get; set; }

        #endregion
        
        public override void Fall(Vector3 fallPosition)
        {
            gameObject.SetActive(true);

            _moveBehaviour.StartMovementTowards(_moveSettings, fallPosition, onReachedTarget);

            void onReachedTarget()
            {
                _reachedTargetActions.OnExecutionCompleted += onReachedTargetActionsCompleted;
                _reachedTargetActions.Execute();
            }

            void onReachedTargetActionsCompleted()
            {
                _reachedTargetActions.OnExecutionCompleted -= onReachedTargetActionsCompleted;

                gameObject.SetActive(false);
                
                OnCompleted?.Invoke(this);
            }
        }
    }
}