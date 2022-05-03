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
        
        public override Action<FallingObjectBase> OnCompleted { get; set; }

        public override void Fall(Vector3 fallPosition)
        {
            gameObject.SetActive(true);

            _moveBehaviour.StartMovementTowards(_moveSettings, fallPosition, onReachedTarget);

            void onReachedTarget()
            {
                _reachedTargetActions.OnExecutionCompleted += onCompleted;
                _reachedTargetActions.Execute();
            }

            void onCompleted()
            {
                _reachedTargetActions.OnExecutionCompleted -= onCompleted;

                gameObject.SetActive(false);
                
                OnCompleted?.Invoke(this);
            }
        }
    }
}